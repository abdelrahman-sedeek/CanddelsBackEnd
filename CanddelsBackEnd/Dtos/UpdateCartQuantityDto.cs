using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Dtos
{
    public class UpdateCartQuantityDto
    {

        [Required]
        public string SessionId { get; set; }

        public int? ProductVariantId { get; set; }
        public int? CustomProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public bool IsValid() =>
            (ProductVariantId.HasValue ^ CustomProductId.HasValue); 

    }
}