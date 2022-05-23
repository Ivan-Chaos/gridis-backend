using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Address : EntityBase
    {
        public Address()
        {
            Residences = new HashSet<Residence>();
        }

        public string BuildingNumber { get; set; } = null!;
        public int StreetId { get; set; }
        public bool IsPrivateBuilding { get; set; }

        public virtual Street Street { get; set; } = null!;
        public virtual ICollection<Residence> Residences { get; set; }
    }
}
