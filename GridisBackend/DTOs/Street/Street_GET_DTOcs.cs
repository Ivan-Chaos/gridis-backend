using GridisBackend.DTOs.District;

namespace GridisBackend.DTOs.Street
{
    public class Street_GET_DTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual District_GET_DTO District { get; set; } = null!;
    }
}
