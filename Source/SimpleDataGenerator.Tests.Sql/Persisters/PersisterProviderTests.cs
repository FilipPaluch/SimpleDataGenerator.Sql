using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Sql.Database.Implementations;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Persisters.Model;
using SimpleDataGenerator.Sql.Providers;

namespace SimpleDataGenerator.Tests.Sql.Persisters
{
    [TestFixture]
    public class PersisterProviderTests
    {
        [Test]
        public void Should_Throw_Exception_When_Sql_Persister_Is_Not_Registered()
        {
            //ARRANGE

            var configuration = new SqlPersisterConfiguration();
            configuration.SqlMappingConfiguration = new SqlMappingConfiguration(typeof (Foo));
            configuration.EntityType = typeof (Foo);

            var sut = new PersisterProvider(new List<SqlPersisterConfiguration>() { configuration }, new DatabaseContext("Test"));

            //ACT && ASSERT

            Assert.Throws<InvalidOperationException>(()=> sut.Get(typeof(Foo2)));

            
        }

        [Test]
        public void Should_Return_Sql_Persister_For_Type()
        {
            //ARRANGE

            var configuration = new SqlPersisterConfiguration();
            configuration.SqlMappingConfiguration = new SqlMappingConfiguration(typeof(Foo));
            configuration.EntityType = typeof(Foo);

            var sut = new PersisterProvider(new List<SqlPersisterConfiguration>() { configuration }, new DatabaseContext("Test"));

            //ACT

            var result = sut.Get(typeof (Foo));

            //ASSERT
            Assert.That(result.EntityType, Is.EqualTo(typeof(Foo)));

        }

        class Foo
        {
             
        }

        class Foo2
        {
             
        }
    }
}
