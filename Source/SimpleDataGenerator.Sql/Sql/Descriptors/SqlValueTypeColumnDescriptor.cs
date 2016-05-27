using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations;
using SimpleDataGenerator.Sql.Models;
using SimpleDataGenerator.Sql.Providers;
using SimpleDataGenerator.Sql.Mapping;

namespace SimpleDataGenerator.Sql.Sql.Descriptors
{
    public class SqlValueTypeColumnDescriptor<TEntity>
    {
        private readonly SqlMappingConfiguration _mappingConfiguration;
        public SqlValueTypeColumnDescriptor(SqlMappingConfiguration mappingConfiguration)
        {
            _mappingConfiguration = mappingConfiguration;
        }

        public IEnumerable<SqlColumnDescription> Describe(TEntity entity)
        {
            foreach (var simpleTypeProperty in GetValueTypeProperties())
            {
                SqlPropertyConfiguration propertyConfiguration;

                if (_mappingConfiguration.TryGetPropertyConfiguration(simpleTypeProperty.Name, out propertyConfiguration))
                {
                    if (ShouldNotSkip(propertyConfiguration) && IsNotAutoIncrementKey(propertyConfiguration))
                    {
                        yield return CreateColumnDescriptionBasedOnConfiguration(simpleTypeProperty, entity, propertyConfiguration);
                    }
                }
                else
                {
                    yield return CreateColumnDescriptionWithoutConfiguration(simpleTypeProperty, entity);
                }

            }
        }

        private static bool IsNotAutoIncrementKey(SqlPropertyConfiguration propertyConfiguration)
        {
            return propertyConfiguration.Key != KeyGenerator.AutoIncrement;
        }

        private static bool ShouldNotSkip(SqlPropertyConfiguration propertyConfiguration)
        {
            return !propertyConfiguration.ShouldSkip;
        }

        private PropertyInfo[] GetValueTypeProperties()
        {
            var valueTypePropertyProvider = new ValueTypePropertyProvider();
            return valueTypePropertyProvider.Get<TEntity>();
        }

        private SqlColumnDescription CreateColumnDescriptionBasedOnConfiguration(PropertyInfo propertyInfo,
         TEntity entity, SqlPropertyConfiguration propertyConfiguration)
        {
            if (IsEnum(propertyInfo))
            {
                return CreateEnumDescription(propertyInfo, entity, propertyConfiguration);
            }
            if (IsBoolen(propertyInfo))
            {
                return CreateBoolenDescription(propertyInfo, entity, propertyConfiguration);
            }
            return new SqlColumnDescription(propertyConfiguration.ColumnName, propertyInfo.GetValue(entity));
        }

        private SqlColumnDescription CreateColumnDescriptionWithoutConfiguration(PropertyInfo propertyInfo,
          TEntity entity)
        {
            if (IsBoolen(propertyInfo))
            {
                return new SqlColumnDescription(propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(entity)));
            }
            if (IsEnum(propertyInfo))
            {
                throw new InvalidOperationException(String.Format("Missing mapping for : {0} in {1}", propertyInfo.Name, typeof(TEntity)));
            }
            return new SqlColumnDescription(propertyInfo.Name, propertyInfo.GetValue(entity));
        }

        #region HelpMethods


        private static SqlColumnDescription CreateBoolenDescription(PropertyInfo propertyInfo, 
            TEntity entity,
            SqlPropertyConfiguration propertyConfiguration)
        {
            return new SqlColumnDescription(propertyConfiguration.ColumnName, Convert.ToInt32(propertyInfo.GetValue(entity)));
        }

        private static bool IsBoolen(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType == typeof(bool);
        }

        private static SqlColumnDescription CreateEnumDescription(PropertyInfo propertyInfo, TEntity entity,
            SqlPropertyConfiguration propertyConfiguration)
        {
            var enumValue = propertyInfo.GetValue(entity);
            var enumType = propertyInfo.PropertyType;
            return new SqlColumnDescription(propertyConfiguration.ColumnName, (int) Enum.Parse(enumType, enumValue.ToString()));
        }

        private static bool IsEnum(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsEnum;
        }

        #endregion
    }
}
