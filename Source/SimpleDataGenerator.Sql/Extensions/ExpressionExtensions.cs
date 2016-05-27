using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Extensions
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo LeftProperty<TEntity, TAssociatedEntity>(
            this Expression<Func<TEntity, TAssociatedEntity, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;
            var leftMemberExpression = binaryExpression.Left as MemberExpression;
            var leftProperty = leftMemberExpression.Member as PropertyInfo;
            return leftProperty;
        }

        public static PropertyInfo LeftExpression<TEntity, TAssociatedEntity>(
            this Expression<Func<TEntity, TAssociatedEntity, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;
            var leftMemberExpression = binaryExpression.Left as MemberExpression;
            var leftExpression = leftMemberExpression.Expression;
            var memberExpression = leftExpression as MemberExpression;
            var leftProperty = memberExpression.Member as PropertyInfo;
            return leftProperty;
        }

        public static PropertyInfo RigthProperty<TEntity, TAssociatedEntity>(
            this Expression<Func<TEntity, TAssociatedEntity, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;
            var rigthMemberExpression = binaryExpression.Right as MemberExpression;
            var rigthProperty = rigthMemberExpression.Member as PropertyInfo;
            return rigthProperty;
        }
    }
}
