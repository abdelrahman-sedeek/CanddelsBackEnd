namespace CanddelsBackEnd.Dtos
{
    public class AddCustomProductToCartDto
    {
        public string SessionId { get; set; }
        public int Quantity { get; set; }
        public string Scent1 { get; set; }
        public string? Scent2 { get; set; }
        public string? Scent3 { get; set; }
        public string? Scent4 { get; set; }
        public int? Weight { get; set; }
    }
}
