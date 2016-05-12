using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces;

namespace SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations
{
    public class SqlPropertyConfiguration : ISqlPropertyConfiguration
    {

        public SqlPropertyConfiguration(PropertyInfo propertyInfo)
        {
            ColumnName = propertyInfo.Name;
            SourcePropertyInfo = propertyInfo;
            ShouldSkip = false;
            Key = KeyGenerator.None;
        }

        public PropertyInfo SourcePropertyInfo { get; private set; }
        public string ColumnName { get; private set; }
        public KeyGenerator Key { get; private set; }

        public bool ShouldSkip { get; private set; }

        protected void SetKeyType(KeyGenerator key)
        {
            Key = key;
        }

        public ISqlPropertyConfiguration ToColumn(string columnName)
        {
            ColumnName = columnName;
            return this;
        }

        public ISqlPropertyConfiguration Skip()
        {
            ShouldSkip = true;
            return this;
        }


    }
}
