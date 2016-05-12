using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Interfaces;

namespace SimpleDataGenerator.Sql.Mapping.AssociationProperty.Interfaces
{
    public interface ISqlAssociationPropertyConfiguration<TEntity, TInverse> : ISqlPropertyConfiguration
    {
        ISqlPropertyConfiguration HasConstraint(
            Expression<Func<TEntity, TInverse, bool>> constraintExpression);
    }
}
