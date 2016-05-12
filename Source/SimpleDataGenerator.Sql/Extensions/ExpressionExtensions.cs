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

            if(binaryExpression == null) throw new InvalidOperationException();

            var leftMemberExpression = binaryExpression.Left as MemberExpression;

            if (leftMemberExpression == null) throw new InvalidOperationException();

            var leftProperty = leftMemberExpression.Member as PropertyInfo;

            if (leftProperty == null) throw new InvalidOperationException();

            return leftProperty;
        }

        public static PropertyInfo LeftExpression<TEntity, TAssociatedEntity>(
            this Expression<Func<TEntity, TAssociatedEntity, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;

            if (binaryExpression == null) throw new InvalidOperationException();

            var leftMemberExpression = binaryExpression.Left as MemberExpression;

            if (leftMemberExpression == null) throw new InvalidOperationException();

            var leftExpression = leftMemberExpression.Expression;

            if (leftExpression == null) throw new InvalidOperationException();

            var memberExpression = leftExpression as MemberExpression;

            if (memberExpression == null) throw new InvalidOperationException();

            var leftProperty = memberExpression.Member as PropertyInfo;

            if (leftProperty == null) throw new InvalidOperationException();

            return leftProperty;
        }

        public static PropertyInfo RigthProperty<TEntity, TAssociatedEntity>(
            this Expression<Func<TEntity, TAssociatedEntity, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;

            if (binaryExpression == null) throw new InvalidOperationException();

            var rigthMemberExpression = binaryExpression.Right as MemberExpression;

            if (rigthMemberExpression == null) throw new InvalidOperationException();

            var rigthProperty = rigthMemberExpression.Member as PropertyInfo;

            if (rigthProperty == null) throw new InvalidOperationException();

            return rigthProperty;
        }
    }
}
