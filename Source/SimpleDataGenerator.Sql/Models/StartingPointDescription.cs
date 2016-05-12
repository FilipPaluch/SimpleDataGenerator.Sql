using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Models
{
    public class StartingPointDescription
    {
        public StartingPointDescription(Type entityType, int numberOfElements)
        {
            EntityType = entityType;
            NumberOfElementsToGenerate = numberOfElements;
        }
        public Type EntityType { get; private set; }
        public int NumberOfElementsToGenerate { get; private set; }
    }
}
