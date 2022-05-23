using GridisBackend.DTOs.Readings;
using GridisBackend.DTOs.Tarrif;

namespace GridisBackend.DTOs.Bill
{
    public class Bill_GET_DTO
    {
        public int Id { get; set; }
        public string BillNumber { get; set; } = null!;
        public int ResidenceId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime GeneratedAt { get; set; }
        public decimal DaySum { get; set; }
        public decimal NightSum { get; set; }
        public decimal TotalSum { get; set; }

        public virtual Reading_GET_DTO Readings { get; set; } = null!;
        public virtual Tarrif_GET_POST_DTO Tarrif { get; set; } = null!;
    }
}
