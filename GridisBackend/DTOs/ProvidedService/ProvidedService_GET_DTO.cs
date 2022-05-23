using GridisBackend.DTOs.ServiceRequest;

namespace GridisBackend.DTOs.ProvidedService
{
    public class ProvidedService_GET_DTO
    {
        public int Id { get; set; }
        public int EngineerId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }

        public virtual ServiceRequest_GET_DTO ServiceRequest { get; set; } = null!;
    }
}
