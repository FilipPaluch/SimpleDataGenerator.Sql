using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Creators;
using SimpleDataGenerator.Sql.Database.Implementations;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Models;
using SimpleDataGenerator.Sql.Persisters.Model;
using SimpleDataGenerator.Sql.Providers;

namespace SimpleDataGenerator.Sql
{
    public class SimpleSqlDataGenerator
    {
        private readonly List<SqlMappingConfiguration> _sqlMappingConfigurations;
        private readonly List<EntityConfiguration> _entityConfigurations;
        private readonly List<StartingPointDescription> _startingTypes = new List<StartingPointDescription>();
        private readonly string _connectionStringName;
        public SimpleSqlDataGenerator(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            _sqlMappingConfigurations = new List<SqlMappingConfiguration>();
            _entityConfigurations = new List<EntityConfiguration>();
        }

        public SimpleSqlDataGenerator WithConfiguration(IEnumerable<SqlMappingConfiguration> configurations)
        {
            _sqlMappingConfigurations.AddRange(configurations);
            return this;
        }

        public SimpleSqlDataGenerator WithConfiguration(SqlMappingConfiguration configuration)
        {
            _sqlMappingConfigurations.Add(configuration);
            return this;
        }

        public SimpleSqlDataGenerator WithConfiguration(IEnumerable<EntityConfiguration> configurations)
        {
            _entityConfigurations.AddRange(configurations);
            return this;
        }

        public SimpleSqlDataGenerator WithConfiguration(EntityConfiguration configuration)
        {
            _entityConfigurations.Add(configuration);
            return this;
        }

        public SimpleSqlDataGenerator StartFor<TEntity>(int numberOfElement)
        {
            _startingTypes.Add(new StartingPointDescription(typeof(TEntity), numberOfElement));
            return this;
        }

        public void Generate()
        {
            var databaseContext = new DatabaseContext(_connectionStringName);
            var persisterConfigurations = CreatePersisterConfigurations();
            var persisterProvider = new PersisterProvider(persisterConfigurations, databaseContext);

            foreach (var startingPoint in _startingTypes)
            {
                var persister = persisterProvider.Get(startingPoint.EntityType);
                for (var i = 0; i < startingPoint.NumberOfElementsToGenerate; i++)
                {
                    persister.Store();
                }
            }  
        }

        private List<SqlPersisterConfiguration> CreatePersisterConfigurations()
        {
            var persisterConfigurationCreator = new SqlPersisterConfigurationCreator(_sqlMappingConfigurations, _entityConfigurations);
            return persisterConfigurationCreator.Create().ToList();
        } 

    }
}
