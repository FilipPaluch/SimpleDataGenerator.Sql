using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration.Extensions
{
    public static class ConnectionStringExtensions
    {
        public static string GetServerName(this string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.DataSource;
        }

        public static string GetDatabaseName(this string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }
    }
}
