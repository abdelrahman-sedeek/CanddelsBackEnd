namespace CanddelsBackEnd.Dtos
{
    public class AddToCartRequest
    {
        public string SessionId {  get; set; }
        public int Quantity {  get; set; }
        public int? ProductVariantId {  get; set; }
        
        // for custom orders
      
    }
}
