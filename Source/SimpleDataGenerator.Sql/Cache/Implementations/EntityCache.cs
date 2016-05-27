using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Cache.Implementations
{
    public class EntityCache<TEntity> : IEntityCache<TEntity>
    {
        private readonly List<TEntity> _cachedEntities;
        private readonly Random _random;

        public EntityCache()
        {
            _cachedEntities = new List<TEntity>();
            _random = new Random();
        }

        public TEntity GetRandomElement()
        {
            var randomEntityNumber = _random.Next(0, _cachedEntities.Count - 1);
            return _cachedEntities[randomEntityNumber];
        }

        public TEntity Add(TEntity entity)
        {
            _cachedEntities.Add(entity);
            return entity;
        }

        public int GetNumberOfElements()
        {
            return _cachedEntities.Count();
        }

        public Type EntityType
        {
            get { return typeof(TEntity); }
        }
    }
}
