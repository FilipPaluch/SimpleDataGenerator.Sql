using System.Collections.Generic;
using System.Linq;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Models;

namespace SimpleDataGenerator.Sql.Sql.Descriptors
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
            var valueTypePropertiesDescription = DescribeValueTypeProperties(entity);

            var associationPropertiesDescription = DescribeAssociationProperties(entity);

            return valueTypePropertiesDescription.Concat(associationPropertiesDescription);
        }

        private IEnumerable<SqlColumnDescription> DescribeValueTypeProperties(TEntity entity)
        {
            var descriptor = new SqlValueTypeColumnDescriptor<TEntity>(_mappingConfiguration);
            return descriptor.Describe(entity);
        }

        private IEnumerable<SqlColumnDescription> DescribeAssociationProperties(TEntity entity)
        {
            var descriptor = new SqlAssociationColumnDescriptor<TEntity>(_mappingConfiguration);
            return descriptor.Describe(entity);

        }
    }
}
