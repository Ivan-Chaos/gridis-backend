namespace GridisBackend.DTOs.MeterModel
{
    public class MeterModel_POST_DTO
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsOutdoors { get; set; }
        public string ModelName { get; set; } = null!;
        public int TarrifsCount { get; set; }
        public long MaximumCapacity { get; set; }
    }
}
