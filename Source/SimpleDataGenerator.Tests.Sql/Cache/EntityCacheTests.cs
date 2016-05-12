using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Sql.Cache.Implementations;

namespace SimpleDataGenerator.Tests.Sql.Cache
{
    [TestFixture]
    public class EntityCacheTests
    {
        [Test]
        public void Should_Return_Correct_Number_Of_Elements_In_Cache()
        {
            //ARRANGE

            var sut = new EntityCache<Foo>();

            var firstElement = new Foo(1);
            var secondElement = new Foo(2);

            sut.Add(firstElement);
            sut.Add(secondElement);

            //ACT

            var result = sut.GetNumberOfElements();

            //ASSERT
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Should_Return_Random_Element_From_Cache()
        {
            //ARRANGE

            var sut = new EntityCache<Foo>();

            var firstElement = new Foo(1);
            var secondElement = new Foo(2);

            sut.Add(firstElement);
            sut.Add(secondElement);

            //ACT

            var result = sut.GetRandomElement();

            //ASSERT
            Assert.IsTrue(result.Equals(firstElement) || result.Equals(secondElement));
        }

        class Foo
        {
            public Foo(int id)
            {
                Id = id;
            }
            public int Id { get; private set; }

            public override bool Equals(object obj)
            {
                return (Foo)obj != null  && (obj as Foo).Id == Id;
            }
        }
    }
}
