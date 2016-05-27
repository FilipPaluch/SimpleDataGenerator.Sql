using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Extensions;
using SimpleDataGenerator.Sql.Models;

namespace SimpleDataGenerator.Sql.Sql.Builders
{
    public class SqlInputQueryBuilder
    {
        private readonly IEnumerable<SqlColumnDescription> _columnDescriptions;
        private readonly string _tableName;

        public SqlInputQueryBuilder(IEnumerable<SqlColumnDescription> descriptions, string tableName)
        {
            _columnDescriptions = descriptions;
            _tableName = tableName;

            if (_columnDescriptions.IsNullOrEmpty())
                throw new InvalidOperationException(string.Format("Missing values to insert for table : {0}", tableName));
        }


        public string Create()
        {
            var sqlBuilder = PetaPoco.Sql.Builder;
            sqlBuilder.Append(String.Format("INSERT INTO [{0}] ", _tableName));

            var columns = _columnDescriptions.Select(x => x.ColumnName).ToArray();
            var values = _columnDescriptions.Select(x => x.Value).ToArray();

            sqlBuilder = sqlBuilder.AppendInputColumn(columns);
            sqlBuilder = sqlBuilder.AppendInputValues(values);

            return sqlBuilder.SQL;
        }


    }
}
