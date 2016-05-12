using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Configuration.TestFixture
{
    public interface ITestFixture
    {
        void SetUp();
        void TearDown();
    }
}
