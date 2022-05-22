using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Tarrif : EntityBase
    {
        public Tarrif()
        {
            Bills = new HashSet<Bill>();
        }

        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTill { get; set; }
        public decimal DayTarrifCost { get; set; }
        public decimal NightTarrifCost { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
