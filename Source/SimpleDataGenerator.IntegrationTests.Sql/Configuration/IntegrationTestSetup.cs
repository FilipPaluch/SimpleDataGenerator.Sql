using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.IntegrationTests.Sql.Configuration.TestFixture;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration
{
    [SetUpFixture]
    public class IntegrationTestSetup<TFixture> where TFixture : ITestFixture, new()
    {
        public TFixture Fixture { get; set; }

        [SetUp]
        public void SetUp()
        {
            Fixture = new TFixture();
            Fixture.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            Fixture.TearDown();
        }
    }
}
