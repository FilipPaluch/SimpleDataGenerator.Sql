using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.AutoIncrement.Model
{
    public class AddressEntity : BaseEntity
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IList<UserEntity> Users { get; set; } 
    }
}
