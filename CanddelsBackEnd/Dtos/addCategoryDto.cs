namespace CanddelsBackEnd.Dtos
{
    public class addCategoryDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
    }
}