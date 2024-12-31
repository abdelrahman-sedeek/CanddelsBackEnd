namespace CanddelsBackEnd.Dtos
{
    public class UpdateProductVariant
    {
        public int Barcode { get; set; }
        public int StockQuantity {  get; set; }
        public int  Weight { get; set; }
        public decimal Price { get; set; }


    }
}
