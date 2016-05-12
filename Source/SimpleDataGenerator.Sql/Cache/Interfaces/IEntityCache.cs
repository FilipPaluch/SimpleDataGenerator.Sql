using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Cache.Interfaces
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
