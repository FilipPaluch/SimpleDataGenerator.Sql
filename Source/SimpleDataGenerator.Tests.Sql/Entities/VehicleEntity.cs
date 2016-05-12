using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Tests.Sql.Entities
{
    public class VehicleEntity
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public string Name { get; set; }

        public int Kilometers { get; set; }

        public string LicensePlate { get; set; }

        public DateTime? ProductionDate { get; set; }

        public Guid ModelId { get; set; }

        public UserEntity User { get; set; }

        public byte[] Thumbnail { get; set; }

        public Guid UserId { get; set; }
    }
}
