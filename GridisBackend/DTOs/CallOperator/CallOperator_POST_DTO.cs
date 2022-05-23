namespace GridisBackend.DTOs.CallOperator
{
    public class CallOperator_POST_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
        public string AssignedPhoneNumber { get; set; } = null!;
    }
}
