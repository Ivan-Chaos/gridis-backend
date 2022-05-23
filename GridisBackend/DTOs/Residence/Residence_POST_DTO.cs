namespace GridisBackend.DTOs.Residence
{
    public class Residence_POST_DTO
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int ResidentId { get; set; }
        public int InstalledMeterId { get; set; }
        public int? EntranceNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public decimal Size { get; set; }
    }
}
