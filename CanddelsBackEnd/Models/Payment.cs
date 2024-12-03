namespace CanddelsBackEnd.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } //enum

        public decimal ShippingPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } // Success, Failed //enum

        public Order Order { get; set; }
    }

}
