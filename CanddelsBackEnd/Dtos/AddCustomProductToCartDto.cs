namespace CanddelsBackEnd.Dtos
{
    public class AddCustomProductToCartDto
    {
        public string SessionId { get; set; }
        public int Quantity { get; set; }
        public List<int> ScentIds { get; set; } 
        public int? Weight { get; set; }
    }
}
