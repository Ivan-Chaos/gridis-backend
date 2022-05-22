namespace GridisBackend.DTOs.District
{
    public class District_POST_DTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
    }
}
