using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Sql;
using SimpleDataGenerator.Sql.Sql.Descriptors;

namespace SimpleDataGenerator.Tests.Sql.Sql
{
    [TestFixture]
    public class SqlColumnDescriptorTests
    {
        [Test]
        public void Should_Describe_Value_Types_Properties_With_Empty_Mapping()
        {
            //ARRANGE

            var foo = new Foo()
            {
                CreatedOn = new DateTime(2016, 01, 01),
                AssociationId = 10,
                Name = "TestName"
            };

            var sut = new SqlColumnDescriptor<Foo>(new SqlMappingConfiguration(typeof(Foo)));


            //ACT

            var result = sut.Describe(foo).OrderBy(x=> x.ColumnName).ToList();

            //ASSERT
            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result[0].ColumnName, Is.EqualTo("AssociationId"));
            Assert.That(result[0].Value, Is.EqualTo(10));

            Assert.That(result[1].ColumnName, Is.EqualTo("CreatedOn"));
            Assert.That(result[1].Value, Is.EqualTo(new DateTime(2016, 01, 01)));

            Assert.That(result[2].ColumnName, Is.EqualTo("Name"));
            Assert.That(result[2].Value, Is.EqualTo("TestName"));
        }

        [Test]
        public void Should_Describe_Value_Types_Properties_Based_On_Mapping()
        {
            //ARRANGE

            var foo = new Foo()
            {
                CreatedOn = new DateTime(2016, 01, 01),
                AssociationId = 10,
                Name = "TestName"
            };

            var mapping = new SqlMappingConfiguration<Foo>();
            mapping.HasProperty(x => x.CreatedOn).ToColumn("TestCreatedOn");
            mapping.HasProperty(x => x.AssociationId).ToColumn("TestFoo2Id");
            mapping.HasProperty(x => x.Name).ToColumn("TestName");

            var sut = new SqlColumnDescriptor<Foo>(mapping);


            //ACT

            var result = sut.Describe(foo).OrderBy(x => x.ColumnName).ToList();

            //ASSERT
            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result[0].ColumnName, Is.EqualTo("TestCreatedOn"));
            Assert.That(result[0].Value, Is.EqualTo(new DateTime(2016, 01, 01)));

            Assert.That(result[1].ColumnName, Is.EqualTo("TestFoo2Id"));
            Assert.That(result[1].Value, Is.EqualTo(10));

            Assert.That(result[2].ColumnName, Is.EqualTo("TestName"));
            Assert.That(result[2].Value, Is.EqualTo("TestName"));
        }

        [Test]
        public void Should_Describe_Object_Association_Properties()
        {
            //ARRANGE

            var foo = new Foo()
            {
                CreatedOn = new DateTime(2016, 01, 01),
                AssociationId = 10,
                Name = "TestName",
                Foo2 = new Foo2()
                {
                    Id = 1
                }
            };

            var mapping = new SqlMappingConfiguration<Foo>();
            mapping.HasAssociation(x => x.Foo2)
                .HasConstraint((foo1, foo2) => foo1.Foo2.Id == foo2.Id)
                .ToColumn("Foo2Id");

            var sut = new SqlColumnDescriptor<Foo>(mapping);


            //ACT

            var result = sut.Describe(foo).OrderBy(x => x.ColumnName).ToList();

            //ASSERT
            Assert.That(result.Count(), Is.EqualTo(4));

            Assert.That(result[0].ColumnName, Is.EqualTo("AssociationId"));
            Assert.That(result[0].Value, Is.EqualTo(10));

            Assert.That(result[1].ColumnName, Is.EqualTo("CreatedOn"));
            Assert.That(result[1].Value, Is.EqualTo(new DateTime(2016, 01, 01)));

            Assert.That(result[2].ColumnName, Is.EqualTo("Foo2Id"));
            Assert.That(result[2].Value, Is.EqualTo(1));

            Assert.That(result[3].ColumnName, Is.EqualTo("Name"));
            Assert.That(result[3].Value, Is.EqualTo("TestName"));
        }

        [Test]
        public void Should_Skip_Properties_Which_Are_Marked_As_AutoIncrement()
        {
            //ARRANGE

            var foo = new Foo()
            {
                CreatedOn = new DateTime(2016, 01, 01),
                AssociationId = 10,
                Name = "TestName"
            };

            var mapping = new SqlMappingConfiguration<Foo>();
            mapping.HasProperty(x => x.CreatedOn).ToColumn("TestCreatedOn");
            mapping.HasProperty(x => x.AssociationId).IsIdentity(KeyGenerator.AutoIncrement);
            mapping.HasProperty(x => x.Name).ToColumn("TestName");

            var sut = new SqlColumnDescriptor<Foo>(mapping);


            //ACT

            var result = sut.Describe(foo).OrderBy(x => x.ColumnName).ToList();

            //ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));

            Assert.That(result[0].ColumnName, Is.EqualTo("TestCreatedOn"));
            Assert.That(result[0].Value, Is.EqualTo(new DateTime(2016, 01, 01)));

            Assert.That(result[1].ColumnName, Is.EqualTo("TestName"));
            Assert.That(result[1].Value, Is.EqualTo("TestName"));
        }

        [Test]
        public void Should_Skip_Properties_Which_Are_Marked_As_Negligible()
        {
            //ARRANGE

            var foo = new Foo()
            {
                CreatedOn = new DateTime(2016, 01, 01),
                AssociationId = 10,
                Name = "TestName"
            };

            var mapping = new SqlMappingConfiguration<Foo>();
            mapping.HasProperty(x => x.CreatedOn).ToColumn("TestCreatedOn");
            mapping.HasProperty(x => x.AssociationId).Skip();
            mapping.HasProperty(x => x.Name).Skip();

            var sut = new SqlColumnDescriptor<Foo>(mapping);


            //ACT

            var result = sut.Describe(foo).OrderBy(x => x.ColumnName).ToList();

            //ASSERT
            Assert.That(result.Count(), Is.EqualTo(1));

            Assert.That(result[0].ColumnName, Is.EqualTo("TestCreatedOn"));
            Assert.That(result[0].Value, Is.EqualTo(new DateTime(2016, 01, 01)));
        }
    }

    class Foo
    {
        public int AssociationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public Foo2 Foo2 { get; set; }
    }

    class Foo2
    {
        public int Id { get; set; }
    }
}
