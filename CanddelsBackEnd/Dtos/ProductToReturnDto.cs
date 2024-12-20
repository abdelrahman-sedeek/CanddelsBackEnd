using CanddelsBackEnd.Models;
using System.ComponentModel;

namespace CanddelsBackEnd.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Scent { get; set; }     
        public bool IsBestSeller { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDailyOffer { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImageUrl { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; } 
        public decimal HighestPriceAfterDiscount { get; set; }
        public decimal LowestPriceAfterDiscount { get; set; }


        public int Stock { get; set; }
        public decimal? HighestPrice { get; set; }
        public decimal? LowestPrice { get; set; } 
        public decimal? HighestPriceAfterDiscount { get; set; }
        public decimal? LowestPriceAfterDiscount { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

 

        public string CategoryName {  get; set; }
        public string CategoryId { get; set; }



    }
}
