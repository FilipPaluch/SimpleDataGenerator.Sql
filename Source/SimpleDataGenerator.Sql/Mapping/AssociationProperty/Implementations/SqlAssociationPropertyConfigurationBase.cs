using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations;

namespace SimpleDataGenerator.Sql.Mapping.AssociationProperty.Implementations
{
    public class SqlAssociationPropertyConfigurationBase : SqlPropertyConfiguration
    {
        public SqlAssociationPropertyConfigurationBase(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        public Type EntityType { get; private set; }
        public PropertyInfo SourceKeyProperty { get; private set; }
        public PropertyInfo AssociatedKeyProperty { get; private set; }

        protected void SetEntityType(Type entityType)
        {
            EntityType = entityType;
        }

        protected void SetSourceKeyProperty(PropertyInfo sourceKeyProperty)
        {
            SourceKeyProperty = sourceKeyProperty;
        }

        protected void SetAssosiatedKeyProperty(PropertyInfo assosiatedKeyProperty)
        {
            AssociatedKeyProperty = assosiatedKeyProperty;
        }
    }
}
