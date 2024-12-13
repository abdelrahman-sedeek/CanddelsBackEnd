using CanddelsBackEnd.Models;
using System.ComponentModel;

namespace CanddelsBackEnd.Dtos
{
    public class ProductToReturnByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? Benfits { get; set; }
        public string? Scent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDailyOffer { get; set; }
        public string ImageUrl { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public string? CalltoAction { get; set; }
        //public virtual ICollection<ProductVariantDto> productVariants { get; set; } = new List<ProductVariantDto>();
        public int? DiscountId { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; } 
        public decimal HighestPriceAfterDiscount { get; set; }
        public decimal LowestPriceAfterDiscount { get; set; }
        //public virtual Discount? Discount { get; set; }

        public int CategoryName { get; set; }
    }
}
