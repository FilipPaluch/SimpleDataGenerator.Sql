using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.PropertiesConfiguration.Model
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Age { get; set; }
        public IList<VehicleEntity> Vehicles { get; set; }
    }
}
