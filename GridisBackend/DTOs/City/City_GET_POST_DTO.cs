namespace GridisBackend.DTOs
{
    public class City_GET_POST_DTO 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long Population { get; set; }    
    }
}
