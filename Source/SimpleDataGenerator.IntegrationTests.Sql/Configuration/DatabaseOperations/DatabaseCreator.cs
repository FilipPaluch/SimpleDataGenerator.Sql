using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration.DatabaseOperations
{
    public class DatabaseCreator
    {
        public void Create(string connectionString)
        {
            var databaseOperations = new DatabaseOperations();
            databaseOperations.DropDatabaseIfExists(connectionString);
            databaseOperations.CreateDatabase(connectionString);
        }
    }
}
