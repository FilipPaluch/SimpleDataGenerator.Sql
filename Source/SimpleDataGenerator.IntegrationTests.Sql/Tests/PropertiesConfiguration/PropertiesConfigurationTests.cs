using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model;
using SimpleDataGenerator.Sql;
using UserEntity = SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model.UserEntity;
using VehicleEntity = SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model.VehicleEntity;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration
{
    [TestFixture]
    public class PropertiesConfigurationTests : IntegrationTestSetup<PropertiesConfigurationTestFixture>
    {
        [Test]
        public void Should_Store_Entities_With_Properties_Configuration()
        {
            //ARRANGE
            Fixture.CreateTables();

            ////Vehicle
            var vehiclePropertiesConfiguration = new EntityConfiguration<VehicleEntity>();

            vehiclePropertiesConfiguration.For(x => x.Name).WithLengthRange(10, 40);
            vehiclePropertiesConfiguration.For(x => x.Mileage).InRange(10, 30);

            var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();

            vehicleMapping.ToTable("Vehicle");
            vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            vehicleMapping.HasAssociation(x => x.User)
                .HasConstraint((vehicle, user) => vehicle.User.Id == user.Id)
                .ToColumn("UserId");

            ////User
            var userPropertiesConfiguration = new EntityConfiguration<UserEntity>();

            userPropertiesConfiguration.For(x => x.Name).WithLength(10);
            userPropertiesConfiguration.For(x => x.Age).InRange(1, 20);
            userPropertiesConfiguration.For(x => x.CreatedOn).WithConstValue(new DateTime(2016, 01, 01));
            var userMapping = new SqlMappingConfiguration<UserEntity>();

            userMapping.ToTable("User");
            userMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            userMapping.SetNumberOfElementsToGenerate(1);

            var sut = new SimpleSqlDataGenerator(Fixture.ConnectionStringName);
            sut.WithConfiguration(vehicleMapping);
            sut.WithConfiguration(userMapping);
            sut.WithConfiguration(vehiclePropertiesConfiguration);
            sut.WithConfiguration(userPropertiesConfiguration);

            sut.StartFor<VehicleEntity>(numberOfElement: 1);

            //ACT

            sut.Generate();

            //ASSERT

            var vehiclesFromDb = Fixture.GetAllFromVehicleEntityTable().ToList();
            Assert.That(vehiclesFromDb.Count(), Is.EqualTo(1));
            Assert.LessOrEqual(vehiclesFromDb[0].Name.Length, 40);
            Assert.GreaterOrEqual(vehiclesFromDb[0].Name.Length, 10);
            Assert.LessOrEqual(vehiclesFromDb[0].Mileage, 30);
            Assert.GreaterOrEqual(vehiclesFromDb[0].Mileage, 10);

            var usersFromDb = Fixture.GetAllFromUserEntityTable().ToList();
            Assert.That(usersFromDb.Count(), Is.EqualTo(1));
            Assert.That(usersFromDb[0].Name.Length, Is.EqualTo(10));
            Assert.LessOrEqual(usersFromDb[0].Age, 20);
            Assert.GreaterOrEqual(usersFromDb[0].Age, 1);
            Assert.That(usersFromDb[0].CreatedOn, Is.EqualTo(new DateTime(2016, 01, 01)));
        }
    }
}
