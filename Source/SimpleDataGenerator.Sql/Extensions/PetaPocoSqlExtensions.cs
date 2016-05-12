using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Extensions
{
    public static class PetaPocoSqlExtensions
    {
        public static PetaPoco.Sql AppendInputColumn(this PetaPoco.Sql sql, string[] columns)
        {
            var columnSqlBuilder = PetaPoco.Sql.Builder;
            foreach (var column in columns)
            {
                columnSqlBuilder.Append(String.Format("[{0}],", column));
            }

            var result = columnSqlBuilder.SQL;

            result = columnSqlBuilder.SQL.Remove(result.Length - 1);

            sql.Append(String.Format("({0})", result));

            return sql;
        }

        public static PetaPoco.Sql AppendInputValues(this PetaPoco.Sql sql, object[] values)
        {
            var valuesSqlBuilder = PetaPoco.Sql.Builder;
            foreach (var value in values)
            {
                if (value == null)
                {
                    valuesSqlBuilder.Append("null,");
                }
                else
                {
                    valuesSqlBuilder.Append(string.Format("'{0}',", value)); 
                }
                
            }

            var result = valuesSqlBuilder.SQL;

            result = valuesSqlBuilder.SQL.Remove(result.Length - 1);

            sql.Append(String.Format("VALUES ({0})", result));

            return sql;
        }
    }
}
