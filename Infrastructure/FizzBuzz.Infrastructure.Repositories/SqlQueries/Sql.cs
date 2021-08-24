using System;
using System.Collections.Generic;
using System.Text;

namespace FizzBuzz.Infrastructure.Repositories.SqlQueries
{
    public static class Sql
    {
        public static string InsertInMetricsTable(string primaryKey) { return @"INSERT INTO fizzbuzz_metric(param, hits) VALUES('" + primaryKey + "'," + 1 + ") ON CONFLICT(param) DO UPDATE SET hits = fizzbuzz_metric.hits + 1;"; }
        public const string MAX_HITS_METRICS = @"SELECT param, MAX(hits) as hits FROM fizzbuzz_metric group by param;";
        public const string PGSQL_HEALTHCHECK_VERSION = @"SELECT version();";
    }
}
