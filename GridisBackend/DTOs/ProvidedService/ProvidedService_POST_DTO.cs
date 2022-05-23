namespace GridisBackend.DTOs.ProvidedService
{
    public class ProvidedService_POST_DTO
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public int EngineerId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}
