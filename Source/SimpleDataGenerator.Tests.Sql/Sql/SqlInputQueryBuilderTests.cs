using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Sql.Extensions;
using SimpleDataGenerator.Sql.Models;
using SimpleDataGenerator.Sql.Sql.Builders;

namespace SimpleDataGenerator.Tests.Sql.Sql
{
    [TestFixture]
    public class SqlInputQueryBuilderTests
    {
        private static readonly object[] SqlColumnDescriptionsListIsNullOrEmptyTestCases =
        {
            new object[] { null },
            new object[] { Enumerable.Empty<SqlColumnDescription>() }
        };

        [Test, TestCaseSource("SqlColumnDescriptionsListIsNullOrEmptyTestCases")]
        public void Should_Throw_Exception_When_Sql_Column_Descriptions_List_Is_Null_Or_Empty(IEnumerable<SqlColumnDescription> sqlColumnDescriptions)
        {
            //ARRANGE & ACT & ASSERT

            Assert.Throws<InvalidOperationException>(() => new SqlInputQueryBuilder(sqlColumnDescriptions, "TableName"));

        }

        [Test]
        public void Should_Not_Throw_Exception_When_Sql_Column_Descriptions_List_Is_Not_Empty()
        {
            //ARRANGE & ACT & ASSERT

            var sqlColumnDescriptions = new List<SqlColumnDescription>(){ new SqlColumnDescription() };

            Assert.DoesNotThrow(() => new SqlInputQueryBuilder(sqlColumnDescriptions, "TableName"));
        }

        [Test]
        public void Should_Create_Sql_Input_Query_Based_On_Columns_Description()
        {
            //ARRANGE

             var fixture = new SqlInputQueryBuilderTestFixture();

            var sut = fixture.ForTable("TestTable")
                             .WithColumnsDescriptions((columnDescriptionBuilder) => columnDescriptionBuilder
                                 .Describe("Id", new Guid("7CDF1C94-1960-466D-9566-91BB4545A89F"))
                                 .Describe("Name", "TestName")
                                 .Describe("Kilometers", 100)
                                 .Describe("Age", 20)
                                 .Describe("CreatedOn", "2016-02-01"))
                             .CreateSut();

            var expectedSqlQuery = "INSERT INTO [TestTable] " +
                                   "([Id],[Name],[Kilometers],[Age],[CreatedOn])" +
                                   "VALUES ('7CDF1C94-1960-466D-9566-91BB4545A89F', 'TestName', '100', '20', '2016-02-01')";

            //ACT

            var result = sut.Create();

            //ASSERT
            StringAssert.AreEqualIgnoringCase(result.RemoveBlanks(), expectedSqlQuery.RemoveBlanks());

        }


    }

    public class SqlInputQueryBuilderTestFixture
    {
        private string _tableName;
        private List<SqlColumnDescription> _columnDescriptions; 
        public SqlInputQueryBuilderTestFixture ForTable(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public SqlInputQueryBuilderTestFixture WithColumnsDescriptions(
            Action<SqlColumnsDescriptionsBuilder> sqlColumnsDescriptionsBuilder)
        {
            var builder = new SqlColumnsDescriptionsBuilder();
            sqlColumnsDescriptionsBuilder.Invoke(builder);
            _columnDescriptions = builder.Build();
            return this;
        }

        public SqlInputQueryBuilder CreateSut()
        {
            return new SqlInputQueryBuilder(_columnDescriptions, _tableName);
        }
    }

    public class SqlColumnsDescriptionsBuilder
    {
        private readonly List<SqlColumnDescription> _sqlColumnsDescriptions = new List<SqlColumnDescription>();

        public SqlColumnsDescriptionsBuilder Describe(string columnName, object value)
        {
            _sqlColumnsDescriptions.Add(new SqlColumnDescription(columnName, value));
            return this;
        }

        public List<SqlColumnDescription> Build()
        {
            return _sqlColumnsDescriptions;
        }
    }
}
