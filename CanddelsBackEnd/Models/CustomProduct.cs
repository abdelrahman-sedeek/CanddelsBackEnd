using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Models
{
    public class CustomProduct:BaseEntity
    {
        public int Weight { get; set; }
        public virtual CartItem CartItem { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<CustomProductScent> CustomProductScents { get; set; } = new List<CustomProductScent>();

    }
}
