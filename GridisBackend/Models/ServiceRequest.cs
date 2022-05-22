using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class ServiceRequest : EntityBase
    {
        public ServiceRequest()
        {
            ProvidedServices = new HashSet<ProvidedService>();
        }

        public int ServiceId { get; set; }
        public int ResidenceId { get; set; }
        public int CallOperatorId { get; set; }
        public int MeterModelId { get; set; }
        public DateTime ReceivedAt { get; set; }

        public virtual CallOperator CallOperator { get; set; } = null!;
        public virtual MeterModel MeterModel { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<ProvidedService> ProvidedServices { get; set; }
    }
}
