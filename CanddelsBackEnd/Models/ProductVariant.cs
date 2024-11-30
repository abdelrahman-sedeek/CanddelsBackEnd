using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CanddelsBackEnd.Models.Product;

namespace CanddelsBackEnd.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
      
        public decimal Barcode { get; set; }

        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; } 
        public Product Product { get; set; }
    }
}
