using System;
using System.Collections.Generic;

namespace SimpleDataGenerator.IntegrationTests.Sql.Tests.Uniqueidentifier.Model
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Age { get; set; }
        public AddressEntity Address { get; set; }
        public IList<VehicleEntity> Vehicles { get; set; }
    }
}
