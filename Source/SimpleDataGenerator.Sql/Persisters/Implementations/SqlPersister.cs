using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Cache;
using SimpleDataGenerator.Sql.Cache.Implementations;
using SimpleDataGenerator.Sql.Database;
using SimpleDataGenerator.Sql.Generators;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Implementations;
using SimpleDataGenerator.Sql.Mapping.AssociationProperty.Model;
using SimpleDataGenerator.Sql.Persisters.Interfaces;
using SimpleDataGenerator.Sql.Persisters.Model;
using SimpleDataGenerator.Sql.Providers;
using SimpleDataGenerator.Sql.Sql;
using SimpleDataGenerator.Sql.Sql.Builders;
using SimpleDataGenerator.Sql.Sql.Descriptors;

namespace SimpleDataGenerator.Sql.Persisters.Implementations
{
    public class SqlPersister<TEntity> : ISqlPersister where TEntity : class 
    {
        private readonly SqlMappingConfiguration _configuration;
        private readonly IEntityCache<TEntity> _cache;
        private readonly PersisterProvider _persisterProvider;
        private readonly EntityConfiguration _entityConfiguration;
        private readonly IDatabaseContext _databaseContext;

        public SqlPersister(SqlPersisterConfiguration sqlPersisterConfiguration,
            PersisterProvider persisterProvider,
            IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _configuration = sqlPersisterConfiguration.SqlMappingConfiguration;
            _cache = new EntityCache<TEntity>();
            _persisterProvider = persisterProvider;
            _entityConfiguration = sqlPersisterConfiguration.EntityConfiguration;
        }

        public Type EntityType { get { return typeof (TEntity); } }

        public object Store()
        {
            if (ShouldReturnFromCache())
            {
                return _cache.GetRandomElement();
            }
            return GenerateEntity();
        }

        private bool ShouldReturnFromCache()
        {
            if (_configuration.NumberOfElementsToGenerate.HasValue)
            {
                return _cache.GetNumberOfElements() >= _configuration.NumberOfElementsToGenerate;
            }
            return false;
        }

        private object GenerateEntity()
        {
            var entity = CreateEntityWithFilledValueTypeProperties();

            entity = FillAssociatedProperties(entity);

            var generatedId = StoreEntityToDatabase(entity);

            AssignIdForAutoIncrementProperty(generatedId, entity);

            AddEntityToCache(entity);

            return entity;
        }

        private TEntity FillAssociatedProperties(TEntity entity)
        {
            foreach (var associationConfiguration in _configuration.AssociationPropertyConfigurations)
            {
                var persister = _persisterProvider.Get(associationConfiguration.EntityType);
                var generatedObject = persister.Store();
                AssignAssociatedProperty(associationConfiguration, entity, generatedObject);
            }

            return entity;
        }

        private TEntity CreateEntityWithFilledValueTypeProperties()
        {
            var dataGenerator = new SimpleTypePropertyAutoFixture<TEntity>();
            dataGenerator.WithConfiguration(_entityConfiguration);
            return dataGenerator.Create();
        }

        private void AssignIdForAutoIncrementProperty(int generatedId, TEntity entity)
        {
            if (IsAutoIncrementKey())
            {
                var keyProperty = _configuration.PropertyConfigurations.First(x => x.Key == KeyGenerator.AutoIncrement);
                keyProperty.SourcePropertyInfo.SetValue(entity, generatedId);
            }
        }

        private bool IsAutoIncrementKey()
        {
            return _configuration.PrimaryKeyProperty.Key == KeyGenerator.AutoIncrement;
        }

        private void AssignAssociatedProperty(SqlAssociationPropertyConfigurationBase associationConfiguration, 
            TEntity currentEntity, 
            object generatedAssociatedEntity)
        {
            if (IsObjectAssociation(associationConfiguration))
            {
                AssignObjectRelation(associationConfiguration, currentEntity, generatedAssociatedEntity);
            }
            else
            {
                //Key association
                var keyValueFromAssociatedEntity = associationConfiguration.AssociatedKeyProperty.GetValue(generatedAssociatedEntity);
                AssignKeyRelation(associationConfiguration, currentEntity, keyValueFromAssociatedEntity);
            }
        }

        private static void AssignKeyRelation(SqlAssociationPropertyConfigurationBase associationConfiguration,
            TEntity currentEntity, object keyValueFromAssociatedEntity)
        {
            associationConfiguration.SourceKeyProperty.SetValue(currentEntity, keyValueFromAssociatedEntity);
        }

        private static void AssignObjectRelation(SqlAssociationPropertyConfigurationBase associationConfiguration,
            TEntity currentEntity, object generatedAssociatedEntity)
        {
            associationConfiguration.SourceKeyProperty.SetValue(currentEntity, generatedAssociatedEntity);
        }

        private static bool IsObjectAssociation(SqlAssociationPropertyConfigurationBase associationConfiguration)
        {
            return associationConfiguration.SourceKeyProperty.PropertyType.IsClass;
        }

        private int StoreEntityToDatabase(TEntity entity)
        {
            var columnsDescriptor = new SqlColumnDescriptor<TEntity>(_configuration);
            var columnsDescription = columnsDescriptor.Describe(entity).ToList();

            var sqlQueryBuilder = new SqlInputQueryBuilder(columnsDescription, _configuration.TableName);

            var query = sqlQueryBuilder.Create();

            using (var database = _databaseContext.GetSession())
            {
                return database.Execute(query);
            }
        }

        private void AddEntityToCache(TEntity entity)
        {
            _cache.Add(entity);
        }
    }
}
