using GridisBackend.DTOs.Person;

namespace GridisBackend.DTOs.Admin
{
    public class Admin_GET_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Person_GET_POST_DTO Person { get; set; } = null!;
    }
}
