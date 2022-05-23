namespace GridisBackend.DTOs.ServiceRequest
{
    public class ServiceRequest_POST_DTO
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ResidenceId { get; set; }
        public int CallOperatorId { get; set; }
        public int MeterModelId { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
