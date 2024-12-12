using CanddelsBackEnd.Models;
using System.ComponentModel;

namespace CanddelsBackEnd.Dtos
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Scent { get; set; }     
        public bool IsBestSeller { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDailyOffer { get; set; }

        public string ImageUrl { get; set; }
        //public virtual ICollection<ProductVariantDto> productVariants { get; set; } = new List<ProductVariantDto>();

        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }


    }
}
