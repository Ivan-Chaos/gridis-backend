using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Bill : EntityBase
    {
        public string BillNumber { get; set; } = null!;
        public int ReadingsId { get; set; }
        public int ResidenceId { get; set; }
        public int TarrifId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime GeneratedAt { get; set; }
        public decimal DaySum { get; set; }
        public decimal NightSum { get; set; }
        public decimal TotalSum { get; set; }

        public virtual Reading Readings { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;
        public virtual Tarrif Tarrif { get; set; } = null!;
    }
}
