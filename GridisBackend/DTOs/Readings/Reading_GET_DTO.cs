using GridisBackend.DTOs.InstalledMeter;

namespace GridisBackend.DTOs.Readings
{
    public class Reading_GET_DTO
    {
        public int Id { get; set; }
        public DateTime DataCollectedAt { get; set; }
        public decimal DayReadings { get; set; }
        public decimal NightReadings { get; set; }

        public virtual InstalledMeter_GET_DTO InstalledMeter { get; set; } = null!;
    }
}
