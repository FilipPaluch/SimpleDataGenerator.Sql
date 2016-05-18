using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Mapping.SingleProperty.Implementations;
using SimpleDataGenerator.Sql.Models;
using SimpleDataGenerator.Sql.Providers;

namespace SimpleDataGenerator.Sql.Sql
{
    public class SqlColumnDescriptor<TEntity>
    {
        private readonly SqlMappingConfiguration _mappingConfiguration;
        public SqlColumnDescriptor(SqlMappingConfiguration mappingConfiguration)
        {
            _mappingConfiguration = mappingConfiguration;
        }

        public IEnumerable<SqlColumnDescription> Describe(TEntity entity)
        {
            var result = new List<SqlColumnDescription>();

            var valueTypePropertiesDescription = DescribeValueTypeProperties(entity).ToList();

            var associationPropertiesDescription = DescribeAssociationProperties(entity).ToList();

            result.AddRange(valueTypePropertiesDescription);
            result.AddRange(associationPropertiesDescription);
            
            return result;
        }

        private IEnumerable<SqlColumnDescription> DescribeValueTypeProperties(TEntity entity)
        {
            foreach (var simpleTypeProperty in GetValueTypeProperties())
            {
                SqlPropertyConfiguration propertyConfiguration;

                if (_mappingConfiguration.TryGetPropertyConfiguration(simpleTypeProperty.Name, out propertyConfiguration))
                {
                    if (!propertyConfiguration.ShouldSkip && propertyConfiguration.Key != KeyGenerator.AutoIncrement)
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

        private IEnumerable<SqlColumnDescription> DescribeAssociationProperties(TEntity entity)
        {
            var sqlColumnDescriptions = new List<SqlColumnDescription>();
            foreach (var associationConfiguration in _mappingConfiguration.AssociationPropertyConfigurations)
            {
                var entityType = associationConfiguration.SourceKeyProperty;
                if (entityType.PropertyType.IsClass)
                {
                    var associatedObject = associationConfiguration.SourceKeyProperty.GetValue(entity);
                    var value = associatedObject.GetType().GetProperty(associationConfiguration.AssociatedKeyProperty.Name).GetValue(associatedObject);
                    sqlColumnDescriptions.Add(new SqlColumnDescription(associationConfiguration.ColumnName, value));
                }
            }
            return sqlColumnDescriptions;
        }

        private PropertyInfo[] GetValueTypeProperties()
        {
            var valueTypePropertyProvider = new ValueTypePropertyProvider();
            var valueTypeProperties = valueTypePropertyProvider.Get<TEntity>();
            return valueTypeProperties;
        }

        private SqlColumnDescription CreateColumnDescriptionWithoutConfiguration(PropertyInfo propertyInfo,
            TEntity entity)
        {
            if (propertyInfo.PropertyType == typeof(bool))
            {
                return new SqlColumnDescription(propertyInfo.Name, Convert.ToInt32(propertyInfo.GetValue(entity)));
            }
            if (propertyInfo.PropertyType.IsEnum)
            {
                throw new InvalidOperationException(String.Format("Missing mapping for : {0} in {1}", propertyInfo.Name, typeof(TEntity)));
            }
            return new SqlColumnDescription(propertyInfo.Name, propertyInfo.GetValue(entity));
        }

        private SqlColumnDescription CreateColumnDescriptionBasedOnConfiguration(PropertyInfo propertyInfo,
            TEntity entity, SqlPropertyConfiguration propertyConfiguration)
        {
            if (propertyInfo.PropertyType.IsEnum)
            {
                var enumValue = propertyInfo.GetValue(entity);
                var enumType = propertyInfo.PropertyType;
                return new SqlColumnDescription(propertyConfiguration.ColumnName, (int)Enum.Parse(enumType, enumValue.ToString()));
            }
            if (propertyInfo.PropertyType == typeof(bool))
            {
                return new SqlColumnDescription(propertyConfiguration.ColumnName, Convert.ToInt32(propertyInfo.GetValue(entity)));
            }
            return new SqlColumnDescription(propertyConfiguration.ColumnName, propertyInfo.GetValue(entity));
        }
    }
}
