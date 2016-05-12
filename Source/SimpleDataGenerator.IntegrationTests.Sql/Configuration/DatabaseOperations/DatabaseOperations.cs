using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.Extensions;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.Scripts;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration.DatabaseOperations
{
    public class DatabaseOperations
    {

        public void CreateDatabase(string connectionString)
        {
            using (var sqlConnection = CreateOpenMasterSqlConnection(connectionString))
            {
                var databaseName = connectionString.GetDatabaseName();

                var readySql = string.Format(SqlQueries.CreateDatabase, databaseName);

                using (var command = new SqlCommand(readySql, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DropDatabaseIfExists(string connectionString)
        {
            using (var sqlConnection = CreateOpenMasterSqlConnection(connectionString))
            {
                var databaseName = connectionString.GetDatabaseName();

                var readySql = string.Format(SqlQueries.DropDatabase, databaseName);

                using (var command = new SqlCommand(readySql, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private SqlConnection CreateOpenMasterSqlConnection(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            return CreateOpenedConnection(connectionStringBuilder.ToString());
        }

        private SqlConnection CreateOpenedConnection(string connectionString)
        {
            var sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            return sqlConnection;
        }
    }
}
