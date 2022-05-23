namespace GridisBackend.DTOs.InstalledMeter
{
    public class InstalledMeter_POST_DTO
    {
        public int Id { get; set; }
        public DateTime InstalationDate { get; set; }
        public string SerialNumber { get; set; } = null!;
        public int ModelId { get; set; }
    }
}
