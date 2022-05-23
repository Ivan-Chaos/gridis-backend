namespace GridisBackend.DTOs.Service
{
    public class Service_GET_POST_DTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}
