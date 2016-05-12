using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Providers
{
    public class ValueTypePropertyProvider
    {
        public PropertyInfo[] Get<TEntity>()
        {
            return typeof(TEntity).GetProperties().Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string)).ToArray();
        }

    }
}
