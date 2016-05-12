using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Extensions;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Interfaces;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces;

namespace SimpleDataGenerator.Sql.Mapping.AssociationProperty.Implementations
{
    public class SqlAssociationPropertyConfiguration<TEntity, TInverse> : SqlAssociationPropertyConfigurationBase, ISqlAssociationPropertyConfiguration<TEntity, TInverse> 
    {
        public SqlAssociationPropertyConfiguration(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
           
        }

        public ISqlPropertyConfiguration HasConstraint(
             Expression<Func<TEntity, TInverse, bool>> constraintExpression)
        {
            SetSourceKeyProperty(constraintExpression.LeftExpression());
            SetAssosiatedKeyProperty(constraintExpression.RigthProperty());
            SetEntityType(typeof(TInverse));
            return this;
        }
    }
}
