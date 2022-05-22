using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class OperatorReading : EntityBase
    {
        public int OperatorId { get; set; }
        public int ReadingsId { get; set; }

        public virtual CallOperator Operator { get; set; } = null!;
        public virtual Reading Readings { get; set; } = null!;
    }
}
