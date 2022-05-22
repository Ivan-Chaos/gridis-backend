using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Engineer : EntityBase
    {
        public Engineer()
        {
            ProvidedServices = new HashSet<ProvidedService>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
        public int DistrictId { get; set; }

        public virtual District District { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<ProvidedService> ProvidedServices { get; set; }
    }
}
