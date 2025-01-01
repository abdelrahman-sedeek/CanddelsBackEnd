namespace CanddelsBackEnd.Dtos
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public decimal Barcode { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
        public int StockQuantity { get; set; }

    }
}
