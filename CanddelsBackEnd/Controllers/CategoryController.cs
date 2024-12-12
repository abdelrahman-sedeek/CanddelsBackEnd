using AutoMapper;
using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Specifications;


namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryToReturnDto>>> GetCategories()
        {
            var spec = new ProductSpesification();
            var categories = await _categoryRepo.GetAllAsync();
            var topCategories = categories.Take(4).ToList();
            var categoriesToReturn = _mapper.Map<List<Category>, List<CategoryToReturnDto>>(topCategories);

            return Ok(categoriesToReturn);
        }
    }
}
