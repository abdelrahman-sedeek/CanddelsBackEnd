namespace CanddelsBackEnd.Models
{
    public class Cart:BaseEntity
    {
      
        public string SessionId { get; set; } // Using string for flexibility and global uniqueness
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
