namespace GridisBackend.DTOs.Street
{
    public class Street_POST_DTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DistrictId { get; set; }

    }
}
