using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.AutoIncrement.Model
{
    public class VehicleEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Mileage { get; set; }
        public UserEntity User { get; set; }
        public IEnumerable<RepairEntity> Repairs { get; set; } 
    }
}
