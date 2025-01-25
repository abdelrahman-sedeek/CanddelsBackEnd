namespace CanddelsBackEnd.Dtos
{
    public class StoreDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
