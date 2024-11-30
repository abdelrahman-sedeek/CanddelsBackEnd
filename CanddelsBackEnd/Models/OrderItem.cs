using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class OrderItem
    {
     
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int productVariantId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Subtotal { get; set; }

            public Order Order { get; set; }
            public ProductVariant productVariants { get; set; }


    }
}
