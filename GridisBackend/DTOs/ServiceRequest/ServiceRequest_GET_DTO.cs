using GridisBackend.DTOs.CallOperator;
using GridisBackend.DTOs.MeterModel;
using GridisBackend.DTOs.Residence;
using GridisBackend.DTOs.Service;

namespace GridisBackend.DTOs.ServiceRequest
{
    public class ServiceRequest_GET_DTO
    {
        public int Id { get; set; }
        public DateTime ReceivedAt { get; set; }

        public virtual CallOperator_GET_DTO CallOperator { get; set; } = null!;
        public virtual MeterModel_GET_DTO MeterModel { get; set; } = null!;
        public virtual Residence_GET_DTO Residence { get; set; } = null!;
        public virtual Service_GET_POST_DTO Service { get; set; } = null!;
    }
}
