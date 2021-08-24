using FizzBuzz.Domain.Entities.Configs;
using FizzBuzz.Domain.Interfaces;
using FizzBuzz.Infrastructure.Repositories.SqlQueries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace FizzBuzz.Infrastructure.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly ILogger<HealthCheckRepository> _logger;
        private string dbconnection;
        public HealthCheckRepository(ILogger<HealthCheckRepository> logger, IOptions<PgConfig> dbConfig)
        {
            _logger = logger;
            dbconnection = dbConfig.Value.ConnectionString;
        }
        public async Task<bool> CheckDatabaseUp()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(dbconnection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(Sql.PGSQL_HEALTHCHECK_VERSION, conn))
                    {
                        conn.Open();
                        var dr = await cmd.ExecuteReaderAsync();
                        if (dr.Read())
                        {
                            return dr["version"] != null;
                        }
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to reach the database", ex);
                return false;
            }
        }
    }
}
