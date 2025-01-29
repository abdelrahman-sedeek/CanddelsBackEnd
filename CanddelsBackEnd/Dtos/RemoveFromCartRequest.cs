using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Dtos
{
    public class RemoveFromCartRequest
    {
        [Required]
        public string SessionId { get; set; }


       
        public int? ProductVariantId { get; set; }
        public int? CustomProductId { get; set; }


        public bool IsValid() =>
            (ProductVariantId.HasValue ^ CustomProductId.HasValue); 
    }
}