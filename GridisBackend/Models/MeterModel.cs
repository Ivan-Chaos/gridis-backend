using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class MeterModel : EntityBase
    {
        public MeterModel()
        {
            InstalledMeters = new HashSet<InstalledMeter>();
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public int ManufacturerId { get; set; }
        public bool IsOutdoors { get; set; }
        public string ModelName { get; set; } = null!;
        public int TarrifsCount { get; set; }
        public long MaximumCapacity { get; set; }

        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public virtual ICollection<InstalledMeter> InstalledMeters { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
