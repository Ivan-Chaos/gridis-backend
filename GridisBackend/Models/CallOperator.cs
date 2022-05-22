using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class CallOperator : EntityBase
    {
        public CallOperator()
        {
            OperatorReadings = new HashSet<OperatorReading>();
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
        public string AssignedPhoneNumber { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<OperatorReading> OperatorReadings { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
