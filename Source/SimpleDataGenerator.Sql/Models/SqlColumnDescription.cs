using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Models
{
    public class SqlColumnDescription
    {
        public SqlColumnDescription()
        {
            
        }

        public SqlColumnDescription(string columnName, object value)
        {
            ColumnName = columnName;
            Value = value;
        }

        public string ColumnName { get; set; }
        public object Value { get; set; }

        public void SetColumnName(string columnName)
        {
            ColumnName = columnName;
        }

        public void SetValue(object value)
        {
            Value = value;
        }
    }
}
