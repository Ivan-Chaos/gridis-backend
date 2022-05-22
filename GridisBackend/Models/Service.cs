using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Service : EntityBase
    {
        public Service()
        {
            ServiceRequests = new HashSet<ServiceRequest>();
        }
        
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }


        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
