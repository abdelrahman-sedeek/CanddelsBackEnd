using CanddelsBackEnd.Validations;
using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Dtos
{
    public class UpdateProduct
    {
        public string Name { get; set; }
        public int ScentId { get; set; }  
        public string? Benefits { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? CallToAction { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100")]
        public decimal? DiscountPercentage { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsDailyOffer { get; set; }
        public int CategoryId { get; set; }

        [AllowedImageExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only JPG, JPEG, and PNG formats are allowed")]
        public IFormFile? Image { get; set; }


    }
}
