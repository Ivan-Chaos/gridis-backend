using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Reading : EntityBase
    {
        public Reading()
        {
            Bills = new HashSet<Bill>();
            OperatorReadings = new HashSet<OperatorReading>();
        }

        public int InstalledMeterId { get; set; }
        public DateTime DataCollectedAt { get; set; }
        public decimal DayReadings { get; set; }
        public decimal NightReadings { get; set; }

        public virtual InstalledMeter InstalledMeter { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<OperatorReading> OperatorReadings { get; set; }
    }
}
