using System.Collections.Generic;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model
{
    public class AddressEntity : BaseEntity
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IList<UserEntity> Users { get; set; } 
    }
}
