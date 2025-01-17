using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Models
{
    public class CustomProduct:BaseEntity
    {
        [Required]
        public string Scent1 { get; set; }
        public string? Scent2 { get; set; }
        public string? Scent3 { get; set; }
        public string? Scent4 { get; set; }
        public int Weight { get; set; }
        public virtual CartItem CartItem { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
