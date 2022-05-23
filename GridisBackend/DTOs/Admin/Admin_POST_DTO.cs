namespace GridisBackend.DTOs.Admin
{
    public class Admin_POST_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
    }
}
