namespace GridisBackend.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
