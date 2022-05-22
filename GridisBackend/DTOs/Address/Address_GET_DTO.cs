namespace GridisBackend.DTOs.Address
{
    public class Address_GET_DTO
    {
        public int Id { get; set; }
        public string BuildingNumber { get; set; } = null!;
        public bool IsPrivateBuilding { get; set; }

        public virtual Street.Street_GET_DTO Street { get; set; } = null!;

    }
}
