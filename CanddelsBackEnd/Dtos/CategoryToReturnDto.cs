using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Dtos
{
    public class CategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
