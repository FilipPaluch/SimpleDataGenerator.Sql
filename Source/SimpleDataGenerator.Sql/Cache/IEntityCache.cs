using System;

namespace SimpleDataGenerator.Sql.Cache
{
    public interface IEntityCache<TEntity> : IEntityCache
    {
        TEntity GetRandomElement();
        int GetNumberOfElements();
        TEntity Add(TEntity entity);
    }

    public interface IEntityCache
    {
        Type EntityType { get; }
    }

}
