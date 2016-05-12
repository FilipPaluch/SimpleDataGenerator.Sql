using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.TestFixture;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier
{
    public class UniqueidentifierPrimaryKeyTestFixture : BaseTestFixture
    {
        public void CreateTables()
        {
            using (var db = GetSession())
            {
                db.Execute(CreateTablesQuery);
            }
        }

        public int GetVehiclesCount()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("count(*)")
                    .From("[Vehicle]").SQL;

                return db.ExecuteScalar<int>(sql);
            }
        }

        public int GetUsersCount()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("count(*)")
                    .From("[User]").SQL;

                return db.ExecuteScalar<int>(sql);
            }
        }

        public int GetRepairsCount()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("count(*)")
                    .From("[Repair]").SQL;

                return db.ExecuteScalar<int>(sql);
            }
        }

        public int GetAddressesCount()
        {
            using (var db = GetSession())
            {
                var sql = PetaPoco.Sql.Builder
                    .Select("count(*)")
                    .From("[Address]").SQL;

                return db.ExecuteScalar<int>(sql);
            }
        }

        private string CreateTablesQuery
        {
            get
            {
                return @"CREATE TABLE [Address]
                        (
                        [Id] uniqueidentifier,
                        [PostalCode] nvarchar(50) NOT NULL,
                        [City] nvarchar(255) NOT NULL,
                        [Country] nvarchar(50) NOT NULL,
                        CONSTRAINT PK_Address PRIMARY KEY CLUSTERED ([Id] ASC)
                        )
                        
                        CREATE TABLE [User]
                        (
                        [Id] uniqueidentifier,
                        [Name] nvarchar(50) NOT NULL,
                        [CreatedOn] datetime NOT NULL,
                        [Age] INT NOT NULL,
                        [AddressId] uniqueidentifier NOT NULL CONSTRAINT FK_User_Address FOREIGN KEY REFERENCES [Address]([Id]),
                        CONSTRAINT PK_User PRIMARY KEY CLUSTERED ([Id] ASC)
                        )
                        
                        CREATE TABLE [Vehicle]
                        (
                        [Id] uniqueidentifier,
                        [Name] nvarchar(50) NOT NULL,
                        [Mileage] INT NOT NULL,
                        [UserId] uniqueidentifier NOT NULL CONSTRAINT FK_Vehicle_User FOREIGN KEY REFERENCES [User]([Id]),
                        CONSTRAINT PK_Vehicle PRIMARY KEY CLUSTERED ([Id] ASC)
                        )
                        
                        CREATE TABLE [Repair]
                        (
                        [Id] uniqueidentifier,
                        [Description] nvarchar(255) NOT NULL,
                        [Price] decimal(5,2) NOT NULL,
                        [Date] datetime NOT NULL,
                        [VehicleId] uniqueidentifier NOT NULL CONSTRAINT FK_Repair_Vehicle FOREIGN KEY REFERENCES Vehicle([Id]),
                        CONSTRAINT PK_Repair PRIMARY KEY CLUSTERED ([Id] ASC)
                        )";
            }
        }
    }
}
