using System.Collections.Generic;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model
{
    public class VehicleEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Mileage { get; set; }
        public UserEntity User { get; set; }
        public IEnumerable<RepairEntity> Repairs { get; set; } 
    }
}
