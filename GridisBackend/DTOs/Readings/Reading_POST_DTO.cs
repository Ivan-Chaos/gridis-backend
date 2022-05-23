namespace GridisBackend.DTOs.Readings
{
    public class Reading_POST_DTO
    {
        public int Id { get; set; }
        public int InstalledMeterId { get; set; }
        public DateTime DataCollectedAt { get; set; }
        public decimal DayReadings { get; set; }
        public decimal NightReadings { get; set; }
    }
}
