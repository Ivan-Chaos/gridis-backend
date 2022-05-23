namespace GridisBackend.DTOs.Bill
{
    public class Bill_POST_DTO
    {
        public int Id { get; set; }
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
    }
}
