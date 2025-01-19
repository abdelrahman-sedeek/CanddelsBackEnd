using AutoMapper;
using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Specifications;
using CanddelsBackEnd.Services;


namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;
        private readonly FileUploadService _fileUploadService;

        public CategoryController(IGenericRepository<Category> categoryRepo, IMapper mapper, FileUploadService fileUploadService)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryToReturnDto>>> GetCategories()
        {
            var spec = new CategorySpecification();
            var categories = await _categoryRepo.GetAllAsync();

            var categoriesToReturn = _mapper.Map<List<Category>, List<CategoryToReturnDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<CategoryToReturnDto>> AddCategory([FromForm] addCategoryDto addCategory)
        {
            if (addCategory is null)
                return BadRequest();

            string? imgUrl = null;

            if (addCategory.Image != null)
            {
                imgUrl = await _fileUploadService.UploadImage(addCategory.Image, "images/categories");
            }

            var addCategoryEntity = new Category
            {
                Description = addCategory.Description,
                Name = addCategory.Name,
                ImageUrl = imgUrl,

            };

            await _categoryRepo.AddAsync(addCategoryEntity);

            return Ok();
        }

        [HttpPut("update-category/{id}")]
        public async Task<ActionResult<CategoryToReturnDto>> UpdateCategory(int id, [FromForm] updateCategory updateCategory)
        {
            if (updateCategory is null)
                return BadRequest();

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            string? imgUrl = category.ImageUrl;

            if (updateCategory.Image != null)
            {
                imgUrl = await _fileUploadService.UploadImage(updateCategory.Image, "images/categories");
            }

            category.Description = updateCategory.Description ?? category.Description;
            category.Name = updateCategory.Name ?? category.Name;
            category.ImageUrl = imgUrl;

            await _categoryRepo.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete("remove-category/{id}")]
        public async Task<ActionResult> RemoveCategory(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            await _categoryRepo.DeleteAsync(id);

            return Ok();
        }
    }

   
}
