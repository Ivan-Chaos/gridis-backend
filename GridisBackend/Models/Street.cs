using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Street : EntityBase
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        public string Name { get; set; } = null!;
        public int DistrictId { get; set; }

        public virtual District District { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
