namespace GridisBackend.DTOs.Tarrif
{
    public class Tarrif_GET_POST_DTO
    {
        public int Id { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTill { get; set; }
        public decimal DayTarrifCost { get; set; }
        public decimal NightTarrifCost { get; set; }

    }
}
