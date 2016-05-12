using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration;
using SimpleDataGenerator.Sql;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier
{
    [TestFixture]
    public class UniqueidentifierPrimaryKeyTests : IntegrationTestSetup<UniqueidentifierPrimaryKeyTestFixture>
    {
        [Test]
        public void Should_Store_Single_Entity_With_Single_Relations()
        {
            //ARRANGE
            Fixture.CreateTables();

            ////Vehicle
            var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();

            vehicleMapping.ToTable("Vehicle");
            vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            vehicleMapping.HasAssociation(x => x.User)
                .HasConstraint((vehicle, user) => vehicle.User.Id == user.Id)
                .ToColumn("UserId");

            ////Repair
            var repairMapping = new SqlMappingConfiguration<RepairEntity>();

            repairMapping.ToTable("Repair");
            repairMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            repairMapping.HasAssociation(x => x.Vehicle)
                .HasConstraint((repair, vehicle) => repair.Vehicle.Id == vehicle.Id)
                .ToColumn("VehicleId");

            ////User
            var userMapping = new SqlMappingConfiguration<UserEntity>();

            userMapping.ToTable("User");
            userMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            userMapping.HasAssociation(x => x.Address)
                .HasConstraint((user, address) => user.Address.Id == address.Id)
                .ToColumn("AddressId");

            ////Address
            var addressMapping = new SqlMappingConfiguration<AddressEntity>();

            addressMapping.ToTable("Address");
            addressMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);

            var sut = new SimpleSqlDataGenerator(Fixture.ConnectionStringName);
            sut.WithConfiguration(vehicleMapping);
            sut.WithConfiguration(userMapping);
            sut.WithConfiguration(addressMapping);
            sut.WithConfiguration(repairMapping);
            sut.StartFor<RepairEntity>(numberOfElement: 1);

            //ACT

            sut.Generate();

            //ASSERT

            Assert.That(Fixture.GetAddressesCount(), Is.EqualTo(1));
            Assert.That(Fixture.GetRepairsCount(), Is.EqualTo(1));
            Assert.That(Fixture.GetUsersCount(), Is.EqualTo(1));
            Assert.That(Fixture.GetVehiclesCount(), Is.EqualTo(1));
        }

        [Test]
        public void Should_Store_Many_Entities_With_Single_Relations()
        {
            //ARRANGE
            Fixture.CreateTables();

            ////Vehicle
            var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();

            vehicleMapping.ToTable("Vehicle");
            vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            vehicleMapping.HasAssociation(x => x.User)
                .HasConstraint((vehicle, user) => vehicle.User.Id == user.Id)
                .ToColumn("UserId");
            vehicleMapping.SetNumberOfElementsToGenerate(1);

            ////Repair
            var repairMapping = new SqlMappingConfiguration<RepairEntity>();

            repairMapping.ToTable("Repair");
            repairMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            repairMapping.HasAssociation(x => x.Vehicle)
                .HasConstraint((repair, vehicle) => repair.Vehicle.Id == vehicle.Id)
                .ToColumn("VehicleId");

            ////User
            var userMapping = new SqlMappingConfiguration<UserEntity>();

            userMapping.ToTable("User");
            userMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            userMapping.HasAssociation(x => x.Address)
                .HasConstraint((user, address) => user.Address.Id == address.Id)
                .ToColumn("AddressId");
            userMapping.SetNumberOfElementsToGenerate(1);

            ////Address
            var addressMapping = new SqlMappingConfiguration<AddressEntity>();

            addressMapping.ToTable("Address");
            addressMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            addressMapping.SetNumberOfElementsToGenerate(1);

            var sut = new SimpleSqlDataGenerator(Fixture.ConnectionStringName);
            sut.WithConfiguration(vehicleMapping);
            sut.WithConfiguration(userMapping);
            sut.WithConfiguration(addressMapping);
            sut.WithConfiguration(repairMapping);
            sut.StartFor<RepairEntity>(numberOfElement: 10);

            //ACT

            sut.Generate();

            //ASSERT

            Assert.That(Fixture.GetAddressesCount(), Is.EqualTo(1));
            Assert.That(Fixture.GetRepairsCount(), Is.EqualTo(10));
            Assert.That(Fixture.GetUsersCount(), Is.EqualTo(1));
            Assert.That(Fixture.GetVehiclesCount(), Is.EqualTo(1));
        }

        [Test]
        public void Should_Store_Many_Entities_With_Many_Relations()
        {
            //ARRANGE
            Fixture.CreateTables();

            ////Vehicle
            var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();

            vehicleMapping.ToTable("Vehicle");
            vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            vehicleMapping.HasAssociation(x => x.User)
                .HasConstraint((vehicle, user) => vehicle.User.Id == user.Id)
                .ToColumn("UserId");
            vehicleMapping.SetNumberOfElementsToGenerate(10);

            ////Repair
            var repairMapping = new SqlMappingConfiguration<RepairEntity>();

            repairMapping.ToTable("Repair");
            repairMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            repairMapping.HasAssociation(x => x.Vehicle)
                .HasConstraint((repair, vehicle) => repair.Vehicle.Id == vehicle.Id)
                .ToColumn("VehicleId");

            ////User
            var userMapping = new SqlMappingConfiguration<UserEntity>();

            userMapping.ToTable("User");
            userMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            userMapping.HasAssociation(x => x.Address)
                .HasConstraint((user, address) => user.Address.Id == address.Id)
                .ToColumn("AddressId");
            userMapping.SetNumberOfElementsToGenerate(10);

            ////Address
            var addressMapping = new SqlMappingConfiguration<AddressEntity>();

            addressMapping.ToTable("Address");
            addressMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Guid);
            addressMapping.SetNumberOfElementsToGenerate(10);

            var sut = new SimpleSqlDataGenerator(Fixture.ConnectionStringName);
            sut.WithConfiguration(vehicleMapping);
            sut.WithConfiguration(userMapping);
            sut.WithConfiguration(addressMapping);
            sut.WithConfiguration(repairMapping);
            sut.StartFor<RepairEntity>(numberOfElement: 10);

            //ACT

            sut.Generate();

            //ASSERT

            Assert.That(Fixture.GetAddressesCount(), Is.EqualTo(10));
            Assert.That(Fixture.GetRepairsCount(), Is.EqualTo(10));
            Assert.That(Fixture.GetUsersCount(), Is.EqualTo(10));
            Assert.That(Fixture.GetVehiclesCount(), Is.EqualTo(10));
        }
    }
}
