using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Database.Interfaces;
using SimpleDataGenerator.Sql.Mapping;
using SimpleDataGenerator.Sql.Persisters.Implementations;
using SimpleDataGenerator.Sql.Persisters.Interfaces;
using SimpleDataGenerator.Sql.Persisters.Model;

namespace SimpleDataGenerator.Sql.Providers
{
    public class PersisterProvider
    {
        private readonly List<ISqlPersister> _sqlPersisters = new List<ISqlPersister>();
        public PersisterProvider(IEnumerable<SqlPersisterConfiguration> persisterConfigurations, IDatabaseContext databaseContext)
        {
            foreach (var configuration in persisterConfigurations)
            {
                var persisterType = typeof(SqlPersister<>).MakeGenericType(configuration.EntityType);
                var persister = (ISqlPersister)Activator.CreateInstance(persisterType, configuration, this, databaseContext);
                _sqlPersisters.Add(persister);
            }
        }

        public ISqlPersister Get(Type entityType)
        {
            var persister = _sqlPersisters.FirstOrDefault(x => x.EntityType == entityType);
            if (persister == null)
                throw new InvalidOperationException(String.Format("Missing mapping for entity type : {0}", entityType));
            return persister;
        }

    }
}
