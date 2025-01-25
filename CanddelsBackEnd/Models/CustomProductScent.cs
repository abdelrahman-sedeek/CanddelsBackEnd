namespace CanddelsBackEnd.Models
{
    public class CustomProductScent
    {
        public int CustomProductId { get; set; }
        public virtual CustomProduct CustomProduct { get; set; }

        public int ScentId { get; set; }
        public virtual Scent Scent { get; set; }
    }
}
