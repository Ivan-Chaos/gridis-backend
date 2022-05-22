using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Manufacturer : EntityBase
    {
        public Manufacturer()
        {
            MeterModels = new HashSet<MeterModel>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<MeterModel> MeterModels { get; set; }
    }
}
