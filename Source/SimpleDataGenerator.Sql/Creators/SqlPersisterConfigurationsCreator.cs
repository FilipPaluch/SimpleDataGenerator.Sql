using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Sql.Persisters.Model;

namespace SimpleDataGenerator.Sql.Creators
{
    public class SqlPersisterConfigurationsCreator
    {
        private readonly IEnumerable<SqlMappingConfiguration> _sqlMappingConfigurations;
        private readonly IEnumerable<EntityConfiguration> _entityConfigurations;
        public SqlPersisterConfigurationsCreator(IEnumerable<SqlMappingConfiguration> sqlMappingConfigurations,
            IEnumerable<EntityConfiguration> entityConfigurations)
        {
            _sqlMappingConfigurations = sqlMappingConfigurations;
            _entityConfigurations = entityConfigurations;

        }

        public IEnumerable<SqlPersisterConfiguration> Create()
        {
            foreach (var mapping in _sqlMappingConfigurations)
            {
                mapping.Validate();
                var sqlPersisterConfiguration = new SqlPersisterConfiguration();
                sqlPersisterConfiguration.EntityType = mapping.EntityType;
                sqlPersisterConfiguration.SqlMappingConfiguration = mapping;
                sqlPersisterConfiguration.EntityConfiguration =
                    _entityConfigurations.FirstOrDefault(x => x.EntityType == mapping.EntityType);

                yield return sqlPersisterConfiguration;
            }
        }

         
    }
}
