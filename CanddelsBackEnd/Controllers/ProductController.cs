using AutoMapper;
using CanddelsBackEnd.Dtos;
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
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        } 
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> Getproducts()
        {
            var spec = new ProductSpesification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var productsToReturn = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);

            return Ok(productsToReturn);
        }
        [HttpGet("porducts/DailyOffers")]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetDailyOfferproducts()
        {
            var spec = new GetDailyofferSpecification(true);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var productsToReturn = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);

            return Ok(productsToReturn);
        }
        [HttpGet("porducts/BestSellers")]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetBestSellerproducts()
        {
            var spec = new ProductSpesification(true);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var productsToReturn = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);

            return Ok(productsToReturn);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnByIdDto>> Getproduct(int id)
        {
            var spec = new ProductSpesification(id);
            var product= await _productRepo.GetByIdWithSpecAsync(spec);
            var productToReturn=_mapper.Map<Product, ProductToReturnByIdDto>(product);
            return Ok(productToReturn);
        }
    }
}
