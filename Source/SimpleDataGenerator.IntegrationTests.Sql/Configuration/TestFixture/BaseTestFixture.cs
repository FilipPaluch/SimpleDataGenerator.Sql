using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using PetaPoco;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.DatabaseOperations;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration.TestFixture
{
    public class BaseTestFixture : ITestFixture
    {
        private readonly DatabaseCreator _databaseCreator;
        private readonly DatabaseDestroyer _databaseDestroyer;

        public BaseTestFixture()
        {
            _databaseCreator = new DatabaseCreator();
            _databaseDestroyer = new DatabaseDestroyer();
        }

        public void SetUp()
        {
            _databaseCreator.Create(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString);

        }

        public void TearDown()
        {
            _databaseDestroyer.Destroy(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString);
        }

        public Database GetSession()
        {
            return new Database("TestConnectionString");
        }

        public string ConnectionStringName
        {
            get { return "TestConnectionString"; }
        }

    }
}
