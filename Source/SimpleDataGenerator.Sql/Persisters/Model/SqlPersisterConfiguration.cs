using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Sql.Mapping;

namespace SimpleDataGenerator.Sql.Persisters.Model
{
    public class SqlPersisterConfiguration
    {
        public Type EntityType { get; set; }
        public SqlMappingConfiguration SqlMappingConfiguration { get; set; }
        public EntityConfiguration EntityConfiguration { get; set; }
    }
}
