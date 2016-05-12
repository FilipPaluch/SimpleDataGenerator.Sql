using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Sql.Specimens;

namespace SimpleDataGenerator.Tests.Sql.Specimens
{
    [TestFixture]
    public class PropertyNameIncluderTests
    {
        [Test]
        public void Should_Return_OmitSpecimen_Class_For_Not_Included_Properties()
        {
            //ARRANGE

            var registeredProperty = typeof (Foo).GetProperty("Name");
            var notRegisteredProperty = typeof(Foo).GetProperty("Kilometers");

            var sut = new PropertyNameIncluder(registeredProperty.Name);

            //ACT

            var result = sut.Create(notRegisteredProperty, null);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(OmitSpecimen)));
        }

        [Test]
        public void Should_Return_NoSpecimen_Class_For_Included_Properties()
        {
            //ARRANGE

            var registeredProperty = typeof(Foo).GetProperty("Name");

            var sut = new PropertyNameIncluder(registeredProperty.Name);

            //ACT

            var result = sut.Create(registeredProperty, null);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        class Foo
        {
            public string Name { get; set; }
            public int Kilometers { get; set; }
        }
    }
}
