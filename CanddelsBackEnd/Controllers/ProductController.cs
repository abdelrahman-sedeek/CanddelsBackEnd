using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Repositories.PorductRepo;
using CanddelsBackEnd.Services;
using CanddelsBackEnd.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace CanddelsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
    
        private readonly IproductRepository _repository;
        private readonly FileUploadService _fileUploadService;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductVariant> _productVariantRepo;
        private readonly IMapper _mapper;

        public ProductController( 
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductVariant> productVariantRepo, 
            IproductRepository repository,
            FileUploadService fileUploadService,IMapper mapper)
        {
            _productRepo = productRepo;
            _productVariantRepo = productVariantRepo;
            _repository = repository;
            _fileUploadService = fileUploadService;
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

        [HttpGet("dashboard-products")]
        public async Task<ActionResult<List<ProductDashboardDto>>> GetproductsForDahboard(
            [FromQuery] ProductParameters ProductPrams)
        {
            var spec = new ProductSpesification(ProductPrams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            
         
            var productsToReturn = _mapper.Map<List<Product>, List<ProductDashboardDto>>(products);

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
            var topProducts = products.Take(3).ToList();
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

        [HttpPost("add-product")]
        public async Task<ActionResult> addProduct([FromForm]AddProductDto product)
        {
            if (product == null) return BadRequest("Product is null");

            string imageUrl = null;

            // Check if the product has an image
            if (product.Image != null)
            {

                imageUrl = await _fileUploadService.UploadImage(product.Image, "images");
            }

            var Addproduct = new Product()
            {
                CategoryId = product.CategoryId,
                Benfits = product.Benfits,
                CalltoAction = product.CalltoAction,
                Description = product.Description,
                DiscountPercentage = product.DiscountPercentage,
                Features = product.Features,
                CreatedAt= DateTime.Now,
                ScentId = product.ScentId,
                IsBestSeller = product.IsBestSeller,
                IsDailyOffer= product.IsDailyOffer,
                Name = product.Name,
                ImageUrl = imageUrl

            };
                
            await _productRepo.AddAsync(Addproduct);
            return Ok();


        }

       

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> updateProduct([FromForm] UpdateProduct product, int id)
        {
            var existingProduct = await _productRepo.GetByIdAsync(id);
            if (existingProduct == null) return NotFound("Product not found");

            if (product.Image is not null)
            {
              
                var imageUrl = await _fileUploadService.UploadImage(product.Image, "images");
                existingProduct.ImageUrl = imageUrl;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Benfits = product.Benefits ?? existingProduct.Benfits;
            existingProduct.Features = product.Features ?? existingProduct.Features;
            existingProduct.CalltoAction = product.CallToAction ?? existingProduct.CalltoAction;
            existingProduct.DiscountPercentage = product.DiscountPercentage;
            existingProduct.ScentId = product.ScentId;
            existingProduct.IsBestSeller = product.IsBestSeller ;
            existingProduct.IsDailyOffer = product.IsDailyOffer;

            await _productRepo.UpdateAsync(existingProduct);
            return Ok();
        }

        [HttpPut("update-productVariant/{id}")]
        public async Task<ActionResult> UpdateProductVariants(int id, List<ProductVariantDto> productVariants)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} does not exist.");

          
            var existingVariants = await _productVariantRepo.GetByProductIdAsync(id);

            if (existingVariants.Any() && productVariants is null)
            {
                await _productVariantRepo.DeleteRangeAsync(existingVariants);
                return Ok();
            }

            var existingVariantDict = existingVariants.ToDictionary(v => v.Id);
            var variantIdsToKeep = new HashSet<int>();
            var newVariants = new List<ProductVariant>();
            var updatedVariants = new List<ProductVariant>();

            foreach (var variantDto in productVariants)
            {
                if (variantDto.Id > 0 && existingVariantDict.TryGetValue(variantDto.Id, out var existingVariant))
                {
                    // Update existing variant
                    existingVariant.Barcode = variantDto.Barcode;
                    existingVariant.Price = variantDto.Price;
                    existingVariant.StockQuantity = variantDto.StockQuantity;
                    existingVariant.Weight = variantDto.Weight;
                    updatedVariants.Add(existingVariant);
                    variantIdsToKeep.Add(variantDto.Id);
                }
                else
                {
                    // Add new variant
                    newVariants.Add(new ProductVariant
                    {
                        ProductId = id,
                        Barcode = variantDto.Barcode,
                        Price = variantDto.Price,
                        StockQuantity = variantDto.StockQuantity,
                        Weight = variantDto.Weight
                    });
                }
            }

            var variantsToDelete = existingVariants
                .Where(v => !variantIdsToKeep.Contains(v.Id))
                .ToList();

            try
            {
                if (variantsToDelete.Any())
                {
                    await _productVariantRepo.DeleteRangeAsync(variantsToDelete);
                }
                if (updatedVariants.Any())
                {
                    await _productVariantRepo.UpdateRangeAsync(updatedVariants);
                }
                if (newVariants.Any())
                {
                    await _productVariantRepo.AddRangeAsync(newVariants);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(500, "An error occurred while updating the product variants.");
            }
        }


        [HttpDelete()]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepo.DeleteAsync(id);
                return Ok("Product deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
       

   
    }

}
