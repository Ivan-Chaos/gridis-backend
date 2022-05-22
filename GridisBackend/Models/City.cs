using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class City : EntityBase
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public string Name { get; set; } = null!;
        public long Population { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
