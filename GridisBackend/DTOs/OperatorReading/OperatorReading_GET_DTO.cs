using GridisBackend.DTOs.CallOperator;
using GridisBackend.DTOs.Readings;

namespace GridisBackend.DTOs.OperatorReadings
{
    public class OperatorReading_GET_DTO
    {
        public int Id { get; set; }

        public virtual CallOperator_GET_DTO Operator { get; set; } = null!;
        public virtual Reading_GET_DTO Readings { get; set; } = null!;
    }
}
