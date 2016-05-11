# SimpleDataGenerator.Sql

SimpleDataGenerator.Sql is an open source library for .NET allow fill SQL Database tables with a larage amount of test data. Its primary goal is to allow developers testing database performance and scaling.  

Nuget: https://www.nuget.org/packages/SimpleDataGenerator.Core/

When writing application, usually you do not know how the system will be working with a large amount of data.
SimpleDataGenerator.Sql makes it easier to test performance of applications before deployment to production. Among other features, it offers a possibility to configuration generated values, based on https://github.com/FilipPaluch/SimpleDataGenerator library.

# Generator

This example illustrates the usage of the library based on simple database model.

~~~

    public class VehicleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mileage { get; set; }
        public UserEntity User { get; set; }
    }

    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Age { get; set; }
        public IList<VehicleEntity> Vehicles { get; set; }
    }
    
~~~

## Mapping

Each entity has to has to have mapping configuration where primary key is defined.
Are two supported primary keys : 'AutoIncrement' or 'Guid'.

~~~
    var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();
    vehicleMapping.ToTable("Vehicle");
    vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.AutoIncrement);
~~~

Additionally library allows configure properties names and number of element to generate.

~~~
    vehicleMapping.HasProperty(x => x.Mileage).ToColumn("MileageColumn");
    vehicleMapping.SetNumberOfElementsToGenerate(10);
~~~


### Associations

Each association in entity has to be mapped. Are two possibilities to define associations:

#### Object association

~~~
     vehicleMapping.HasAssociation(x => x.User)
                   .HasConstraint((vehicle, user) => vehicle.User.Id == user.Id)
                   .ToColumn("UserId");
~~~

#### Key association

~~~
     vehicleMapping.HasAssociation(x => x.UserId)
                   .HasConstraint<UserEntity>((vehicle, user) => vehicle.User.Id == user.Id)
                   .ToColumn("UserId");
~~~

## Generation

First step: SimpleSqlDataGenerator initialization

~~~

var generator = new SimpleSqlDataGenerator(dbConnectionString);

~~~

Second step: Applaying mapping configuration

~~~

generator.WithConfiguration(vehicleMapping);

~~~

Third step: Selecting the starting point, so that it will be possible to generate the entire graph data. 
It's possibility two choose multiple points.

~~~

generator.StartFor<VehicleEntity>(numberOfElement: 10);

~~~

Last step: Generate!

~~~

 generator.Generate();

~~~

## Generated values configuration

#### Properties configuration

~~~

 var vehiclePropertiesConfiguration = new EntityConfiguration<VehicleEntity>();

 vehiclePropertiesConfiguration.For(x => x.Name).WithLengthRange(10, 40);
 vehiclePropertiesConfiguration.For(x => x.Mileage).InRange(10, 30);

~~~

#### Adding configuration to generator

~~~
generator.WithConfiguration(vehiclePropertiesConfiguration);
~~~

## Full Example

~~~

    ////Vehicle
    var vehiclePropertiesConfiguration = new EntityConfiguration<VehicleEntity>();

    vehiclePropertiesConfiguration.For(x => x.Name).WithLengthRange(10, 40);
    vehiclePropertiesConfiguration.For(x => x.Mileage).InRange(10, 30);

    var vehicleMapping = new SqlMappingConfiguration<VehicleEntity>();

    vehicleMapping.ToTable("Vehicle");
    vehicleMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.AutoIncrement);
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
    userMapping.HasProperty(x => x.Id).IsIdentity(KeyGenerator.AutoIncrement);
    userMapping.SetNumberOfElementsToGenerate(1);

    var sut = new SimpleSqlDataGenerator(Fixture.ConnectionStringName);
    sut.WithConfiguration(vehicleMapping);
    sut.WithConfiguration(userMapping);
    sut.WithConfiguration(vehiclePropertiesConfiguration);
    sut.WithConfiguration(userPropertiesConfiguration);

    sut.StartFor<VehicleEntity>(numberOfElement: 10);

    //ACT

    sut.Generate();

~~~

In example above will be generated ten vehicles and single user which will be associated with each vehicle.

