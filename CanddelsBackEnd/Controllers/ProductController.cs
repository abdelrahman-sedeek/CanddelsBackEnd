using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Repositories.PorductRepo;
using CanddelsBackEnd.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IproductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo,IproductRepository repository,IMapper mapper)
        {
            _productRepo = productRepo;
            _repository = repository;
            _mapper = mapper;
        } 
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> Getproducts(
            [FromQuery] ProductParameters ProductPrams)
        {
            var spec = new ProductSpesification(ProductPrams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            
         
            var productsToReturn = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);

            return Ok(new
            {
                PageIndex = ProductPrams.PageIndex,
                PageSize = ProductPrams.PageSize,
                TotalCount = await _productRepo.CountAsync(spec.Criteria),
                Data = productsToReturn
            });
        }

        [HttpGet("homeProducts")]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetHomeproducts()
        {
            var spec = new ProductSpesification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var topProducts = products.Take(6).ToList();
            var productsToReturn = _mapper.Map<List<Product>, List<ProductToReturnDto>>(topProducts);

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

        [HttpGet("scents")]
        public async Task<ActionResult<List<string>>> GetScents()
        {
           var scents = await _repository.GetScentsAsync();

            return Ok(scents);
        }

    }

}
