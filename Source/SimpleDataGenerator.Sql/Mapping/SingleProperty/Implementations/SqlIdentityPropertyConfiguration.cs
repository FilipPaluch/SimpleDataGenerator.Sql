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
    public class SqlIdentityPropertyConfiguration : SqlPropertyConfiguration, ISqlIdentityPropertyConfiguration
    {
        public SqlIdentityPropertyConfiguration(PropertyInfo propertyInfo)
            :base(propertyInfo)
        {
            
        }

        public ISqlPropertyConfiguration IsIdentity(KeyGenerator keyGenerator)
        {
            SetKeyType(keyGenerator);
            return this;
        }
    }
}
