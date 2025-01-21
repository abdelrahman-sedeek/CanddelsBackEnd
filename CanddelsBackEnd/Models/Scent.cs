namespace CanddelsBackEnd.Models
{
    public class Scent:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
