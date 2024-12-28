using CanddelsBackEnd.Models;
using System.ComponentModel;

namespace CanddelsBackEnd.Dtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? Benfits { get; set; }
        public string? Scent { get; set; }

        public bool IsBestSeller { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDailyOffer { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CalltoAction { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public int CategoryId { get; set; }
    }
}
