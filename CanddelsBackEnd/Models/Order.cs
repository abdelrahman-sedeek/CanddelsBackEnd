namespace CanddelsBackEnd.Models
{
    public class Order : BaseEntity

    {
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public string OrderStatus { get; set; } 
        public string PaymentStatus { get; set; } 
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public int ShippingDetailId { get; set; }
        public virtual ShippingDetail ShippingDetail { get; set; }

    }
}
