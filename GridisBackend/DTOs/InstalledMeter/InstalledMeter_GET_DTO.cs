using GridisBackend.DTOs.MeterModel;

namespace GridisBackend.DTOs.InstalledMeter
{
    public class InstalledMeter_GET_DTO
    {
        public int Id { get; set; }
        public DateTime InstalationDate { get; set; }
        public string SerialNumber { get; set; } = null!;

        public virtual MeterModel_GET_DTO Model { get; set; } = null!;
    }
}
