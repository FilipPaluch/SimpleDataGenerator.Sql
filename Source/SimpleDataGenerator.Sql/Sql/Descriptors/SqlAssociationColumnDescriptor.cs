using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Models;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Implementations;

namespace SimpleDataGenerator.Sql.Sql.Descriptors
{
    public class SqlAssociationColumnDescriptor<TEntity>
    {
       private readonly SqlMappingConfiguration _mappingConfiguration;
       public SqlAssociationColumnDescriptor(SqlMappingConfiguration mappingConfiguration)
       {
           _mappingConfiguration = mappingConfiguration;
       }

       public IEnumerable<SqlColumnDescription> Describe(TEntity entity)
       {
           foreach (var associationConfiguration in _mappingConfiguration.AssociationPropertyConfigurations)
           {
               var entityType = associationConfiguration.SourceKeyProperty;
               if (IsClassType(entityType))
               {
                   var associatedObject = associationConfiguration.SourceKeyProperty.GetValue(entity);
                   var value = GetValueFromAssociatedObject(associatedObject, associationConfiguration);
                   yield return new SqlColumnDescription(associationConfiguration.ColumnName, value);
               }
           }
       }

        private object GetValueFromAssociatedObject(object associatedObject, 
            SqlAssociationPropertyConfigurationBase associationConfiguration)
        {
            return associatedObject.GetType().GetProperty(associationConfiguration.AssociatedKeyProperty.Name).GetValue(associatedObject);
        }

        private bool IsClassType(PropertyInfo entityType)
        {
            return entityType.PropertyType.IsClass;
        }
    }
}
