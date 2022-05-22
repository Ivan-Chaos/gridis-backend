using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class ProvidedService : EntityBase
    {
        public int ServiceRequestId { get; set; }
        public int EngineerId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }

        public virtual Engineer Engineer { get; set; } = null!;
        public virtual ServiceRequest ServiceRequest { get; set; } = null!;
    }
}
