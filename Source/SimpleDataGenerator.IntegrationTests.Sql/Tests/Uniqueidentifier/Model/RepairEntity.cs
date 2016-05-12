using System;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model
{
    public class RepairEntity : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
