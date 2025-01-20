using CanddelsBackEnd.Models;
using System.ComponentModel;

namespace CanddelsBackEnd.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ScentId { get; set; }     
        public bool IsBestSeller { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDailyOffer { get; set; }

        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? Benfits { get; set; }
        public string? CalltoAction { get; set; }

        public string ImageUrl { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; } 
        public decimal HighestPriceAfterDiscount { get; set; }
        public decimal LowestPriceAfterDiscount { get; set; }
        public string CategoryName {  get; set; }
        public string CategoryId { get; set; }
        public decimal? DiscountPercentage { get; set; }

        
    }
}
