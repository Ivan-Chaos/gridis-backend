using GridisBackend.DTOs.Person;

namespace GridisBackend.DTOs.CallOperator
{
    public class CallOperator_GET_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AssignedPhoneNumber { get; set; } = null!;

        public virtual Person_GET_POST_DTO Person { get; set; } = null!;
    }
}
