using GridisBackend.DTOs.Manufacturer;

namespace GridisBackend.DTOs.MeterModel
{
    public class MeterModel_GET_DTO
    { 
        public int Id  { get; set; }
        public bool IsOutdoors { get; set; }
        public string ModelName { get; set; } = null!;
        public int TarrifsCount { get; set; }
        public long MaximumCapacity { get; set; }

        public virtual Manufacturer_GET_POST_DTO Manufacturer { get; set; } = null!;

    }
}
