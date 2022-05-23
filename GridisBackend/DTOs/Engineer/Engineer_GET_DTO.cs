using GridisBackend.DTOs.District;
using GridisBackend.DTOs.Person;

namespace GridisBackend.DTOs.Engineer
{
    public class Engineer_GET_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual District_GET_DTO District { get; set; } = null!;
        public virtual Person_GET_POST_DTO Person { get; set; } = null!;
    }
}
