using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class Discount
    {
       
            public int Id { get; set; }
            public string Name { get; set; } // Ex "Daily Discount"
            public decimal DiscountAmount { get; set; } // Fixed amount or percentage
            public bool IsPercentage { get; set; } // True for percentage False for fixed amount
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool IsDaily { get; set; } // True if this is a daily discount
            public int? ProductId { get; set; } // Optional (null if applied to all products)

            public Product Product { get; set; }
  

    }
}
