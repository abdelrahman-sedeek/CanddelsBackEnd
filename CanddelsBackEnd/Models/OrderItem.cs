using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class OrderItem:BaseEntity
    {
     
        public int OrderId { get; set; }
        public int productVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public virtual Order? Order { get; set; }
        public virtual ProductVariant? productVariant { get; set; }


    }
}
