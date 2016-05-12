using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Persisters.Interfaces
{
    public interface ISqlPersister
    {
        Type EntityType { get; }
        object Store();
    }
}
