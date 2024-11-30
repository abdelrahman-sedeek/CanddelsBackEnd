namespace CanddelsBackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; } // Pending, Shipped, Delivered
        public string PaymentStatus { get; set; } // Paid, Unpaid
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ShippingDetail ShippingDetail { get; set; }
        public Payment Payment { get; set; }
    }
}
