using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Payment : EntityBase
    {
        public int InstalledMeterId { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal ReceiptNumber { get; set; }
        public decimal PaidSum { get; set; }


        public virtual InstalledMeter InstalledMeter { get; set; } = null!;
    }
}
