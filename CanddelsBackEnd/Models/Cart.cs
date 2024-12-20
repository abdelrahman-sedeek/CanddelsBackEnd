namespace CanddelsBackEnd.Models
{
    public class Cart:BaseEntity
    {

        
        public string SessionId { get; set; } // Using string for flexibility and global uniqueness



        public static readonly TimeSpan ExpirationDuration = TimeSpan.FromHours(2); 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public Cart()
        {
            ExpiresAt = CreatedAt.Add(ExpirationDuration);
        }

    }
}
