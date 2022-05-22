using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Residence : EntityBase
    {
        public Residence()
        {
            Bills = new HashSet<Bill>();
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public int AddressId { get; set; }
        public int ResidentId { get; set; }
        public int InstalledMeterId { get; set; }
        public int? EntranceNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public decimal Size { get; set; }


        public virtual Address Address { get; set; } = null!;
        public virtual InstalledMeter InstalledMeter { get; set; } = null!;
        public virtual Person Resident { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
