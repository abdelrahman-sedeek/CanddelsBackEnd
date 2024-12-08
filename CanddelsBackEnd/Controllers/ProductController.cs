using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        public ProductController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;

        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Getproducts()
        {
            var spec = new ProductSpesification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Getproduct(int id)
        {
            var spec = new ProductSpesification(id);
            var product= await _productRepo.GetByIdWithSpecAsync(spec);
            return Ok(product);
        }
    }
}
