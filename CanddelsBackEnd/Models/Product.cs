using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml;

namespace CanddelsBackEnd.Models
{
    public class Product
    {
     
            public int Id { get; set; }
           
            public string Name { get; set; }
            public string Description { get; set; }
           
            public decimal BasePrice { get; set; } // Price for the default weight
            [DefaultValue(100)]
            public decimal DefaultWeight { get; set; }
          
            public string Scent { get; set; }

            [DefaultValue(false)]
            public bool IsDaily { get; set; }
            [DefaultValue(false)] 
            public bool IsBestSeller{ get; set; }
            public string ImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
            public JsonDocument Benefits { get; set; }

            public ICollection<OrderItem> OrderItems { get; set; }
            public ICollection<Discount> Discounts { get; set; }
            public ICollection<ProductVariant> productVariants { get; set; }

  

    }
}
