using GridisBackend.Models;

namespace GridisBackend.DTOs.District
{
    public class District_GET_DTO
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual City_GET_POST_DTO City { get; set; } = null!;
    }
}
