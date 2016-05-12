using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SimpleDataGenerator.Sql.Providers;

namespace SimpleDataGenerator.Tests.Sql.Providers
{
    [TestFixture]
    public class ValueTypePropertyProviderTests
    {
        [Test]
        public void Should_Return_Simple_Type_Properties_From_Object()
        {
            //ARRANGE
            var sut = new ValueTypePropertyProvider();

            //ACT

            var result = sut.Get<Foo>();

            //ASSERT
            Assert.That(result.Length, Is.EqualTo(17));
            Assert.That(result.Select(x => x.PropertyType).All(p=> ExpectedTypes.Contains(p)));
        }

        class Foo
        {
            //Simple Types
            public bool TestBool { get; set; }
            public char TestChar { get; set; }
            public DateTime TestDateTime { get; set; }
            public TimeSpan TestTimeSpan { get; set; }
            public decimal TestDecimal { get; set; }
            public double TestDouble { get; set; }
            public Int16 TestInt16 { get; set; }
            public Int32 TestInt32 { get; set; }
            public Int64 TestInt64 { get; set; }
            public SByte TestSSbyte { get; set; }
            public Single TestSingle { get; set; }
            public string TestString { get; set; }
            public UInt16 TestUInt16 { get; set; }
            public UInt32 TestUInt32 { get; set; }
            public UInt64 TestUInt64 { get; set; }
            public Guid TestGuid { get; set; }
            public Guid? TestNullableGuid { get; set; }

            public object TestObject { get; set; }
            public ICollection<object> TestCollection { get; set; }
            public byte[] TestBytes { get; set; }
        }

        IEnumerable<Type> ExpectedTypes
        {
            get
            {
                yield return typeof(bool);
                yield return typeof(TimeSpan);
                yield return typeof(char);
                yield return typeof(DateTime);
                yield return typeof(decimal);
                yield return typeof(double);
                yield return typeof(Int16);
                yield return typeof(Int32);
                yield return typeof(Int64);
                yield return typeof(SByte);
                yield return typeof(Single);
                yield return typeof(string);
                yield return typeof(UInt16);
                yield return typeof(UInt32);
                yield return typeof(UInt64);
                yield return typeof(Guid);
                yield return typeof(Guid?);
            }
        } 
    }
}
