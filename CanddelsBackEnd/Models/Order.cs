namespace CanddelsBackEnd.Models
{
    public class Order : BaseEntity

    {
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public string OrderStatus { get; set; } // Pending, Delivered //enum
        public string PaymentStatus { get; set; } // Paid, Unpaid //enum
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public int ShippingDetailId { get; set; }
        public virtual ShippingDetail ShippingDetail { get; set; }

        //public int PaymentId { get; set; }
        //public virtual Payment Payment { get; set; }
    }
}
