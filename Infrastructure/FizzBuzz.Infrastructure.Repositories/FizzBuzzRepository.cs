using FizzBuzz.Domain.Entities;
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
    public class FizzBuzzRepository : IFizzBuzzRepository
    {
        private readonly ILogger<FizzBuzzRepository> _logger;
       
        private string dbconnection;
        public FizzBuzzRepository(ILogger<FizzBuzzRepository> logger, IOptions<PgConfig> dbConfig)
        {
            _logger = logger;
            dbconnection = dbConfig.Value.ConnectionString;
        }

        /// <summary>
        /// Allow to get the fizzbuzz sequence which is more called
        /// </summary>
        /// <returns>a fizzbuzz sequence</returns>
        public async Task<FizzBuzzModel> GetFizzBuzzMaxCalls()
        {
            FizzBuzzModel fbm = null;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(dbconnection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(Sql.MAX_HITS_METRICS, conn))
                    {
                        conn.Open();
                        var dr = await cmd.ExecuteReaderAsync();
                        if (dr.Read())
                        {
                            if (dr["param"] == null)
                                _logger.LogError("Unable to get primary key from GetFizzBuzzMaxCalls");

                            if (dr["hits"] == null)
                                _logger.LogError("Unable to get hits from GetFizzBuzzMaxCalls");

                            fbm = new FizzBuzzModel(primaryKey: dr["param"].ToString(), hits: Convert.ToInt32(dr["hits"]));
                        }
                        conn.Close();
                    }
                }

                return fbm;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to create the table fizzbuzz_metric or to connect to the database", ex);
                return fbm;
            }
        }

        /// <summary>
        /// Allow to insert all the fizzbuzz sequence in a dedicated database
        /// </summary>
        /// <param name="fizzBuzzModel">the fizzbuzz model currently called</param>
        /// <returns>boolean to know if the sequence is correctly inserted or updated</returns>
        public async Task<bool> InsertFizzBuzz(FizzBuzzModel fizzBuzzModel)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(dbconnection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(Sql.InsertInMetricsTable(fizzBuzzModel.PrimaryKey), conn))
                    {
                        conn.Open();
                        await cmd.ExecuteNonQueryAsync();
                        conn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to insert in the table fizzbuzz_metric or to connect to the database", ex);
                return false;
            }
        }
    }
}
