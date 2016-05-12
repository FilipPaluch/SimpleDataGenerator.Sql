using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.AutoIncrement.Model
{
    public class RepairEntity : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
