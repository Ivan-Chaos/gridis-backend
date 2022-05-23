using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class InstalledMeter : EntityBase
    {
        public InstalledMeter()
        {
            Readings = new HashSet<Reading>();
            Residences = new HashSet<Residence>();
        }

        public DateTime InstalationDate { get; set; }
        public string SerialNumber { get; set; } = null!;
        public int ModelId { get; set; }

        public virtual MeterModel Model { get; set; } = null!;
        public virtual ICollection<Reading> Readings { get; set; }
        public virtual ICollection<Residence> Residences { get; set; }
    }
}
