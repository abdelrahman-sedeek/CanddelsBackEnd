namespace CanddelsBackEnd.Dtos
{
    public class RemoveFromCartRequest
    {
        public string SessionId { get; set; }
        public int ProductVariantId { get; set; }
    }
}