namespace GridisBackend.DTOs.Address
{
    public class Address_POST_DTO
    {
        public int Id { get; set; }
        public string BuildingNumber { get; set; } = null!;
        public int StreetId { get; set; }
        public bool IsPrivateBuilding { get; set; }

    }
}
