using System.ComponentModel.DataAnnotations;
using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class Discount:BaseEntity
    {    
        
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; } 
        public decimal? PriceAfterDiscount { get; set; }
        public bool? IsDiscounted { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }    
  
    }
}
