using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class ProductVariant : BaseEntity
    {
 
        public decimal Barcode { get; set; }
       
        public int StockQuantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }

        public int? ProductId { get; set; }

        public  Product Product { get; set; }

        public int? CartItemId { get; set; }
        public virtual CartItem CartItem { get; set; }

        public int? OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }

        public decimal? PriceAfterDiscount {  get; set; }
       

      


    }
}
