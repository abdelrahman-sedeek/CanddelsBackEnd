using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Dtos
{
    public class AddProductVariantsDto
    {
        public decimal Barcode { get; set; }
        public int StockQuantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }

    }
}
