using CanddelsBackEnd.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Dtos
{
    public class AddProductDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Product name must be at least 3 characters.")]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? Benfits { get; set; }
        public string? Scent { get; set; }

        public bool IsBestSeller { get; set; }
        public bool IsDailyOffer { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CalltoAction { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }

}
