using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model
{
    public class VehicleEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Mileage { get; set; }
        public UserEntity User { get; set; }
    }
}
