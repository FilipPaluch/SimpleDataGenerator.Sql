using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.TestFixture;
using SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration
{
    public class PropertiesConfigurationTestFixture : BaseTestFixture
    {
        public void CreateTables()
        {
            using (var db = GetSession())
            {
                db.Execute(CreateTablesQuery);
            }
        }

        private string CreateTablesQuery
        {
            get
            {
                return @"CREATE TABLE [User]
                        (
                        [Id] uniqueidentifier,
                        [Name] nvarchar(50) NOT NULL,
                        [CreatedOn] datetime NOT NULL,
                        [Age] INT NOT NULL,
                        CONSTRAINT PK_User PRIMARY KEY CLUSTERED ([Id] ASC)
                        )
                        
                        CREATE TABLE [Vehicle]
                        (
                        [Id] uniqueidentifier,
                        [Name] nvarchar(50) NOT NULL,
                        [Mileage] INT NOT NULL,
                        [UserId] uniqueidentifier NOT NULL CONSTRAINT FK_Vehicle_User FOREIGN KEY REFERENCES [User]([Id]),
                        CONSTRAINT PK_Vehicle PRIMARY KEY CLUSTERED ([Id] ASC)
                        )";
            }
        }

        public IEnumerable<VehicleEntity> GetAllFromVehicleEntityTable()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("*")
                    .From("[Vehicle]").SQL;

                return db.Query<VehicleEntity>(sql).ToList();
            }
        }

        public IEnumerable<UserEntity> GetAllFromUserEntityTable()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("*")
                    .From("[User]").SQL;

                return db.Query<UserEntity>(sql).ToList();
            }
        } 
    }
}
