namespace GridisBackend.DTOs.Engineer
{
    public class Engineer_POST_DTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
        public int DistrictId { get; set; }
    }
}
