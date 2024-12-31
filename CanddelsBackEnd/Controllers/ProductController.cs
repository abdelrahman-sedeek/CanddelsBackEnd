using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Repositories.PorductRepo;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductVariant> _productVariantRepo;
        private readonly IMapper _mapper;

        public ProductController( 
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductVariant> productVariantRepo, 
            IproductRepository repository,
            IWebHostEnvironment webHostEnvironment,IMapper mapper)
        {
            _productRepo = productRepo;
            _productVariantRepo = productVariantRepo;
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost("add-product")]
        public async Task<ActionResult> addProduct([FromForm]AddProductDto product)
        {
            if (product == null) return BadRequest("Product is null");

            string imageUrl = null;

            // Check if the product has an image
            if (product.Image != null)
            {
                // Validate Image Type and Size
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
                if (!allowedTypes.Contains(product.Image.ContentType))
                {
                    return BadRequest("Invalid file type. Only JPEG, PNG, and JPG are allowed.");
                }

                if (product.Image.Length > 5 * 1024 * 1024) // 5MB size limit
                {
                    return BadRequest("File size exceeds 5MB.");
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;

                var filepath = Path.Combine(uploadsFolder, uniqueFileName);

                // Ensure directory exists
                Directory.CreateDirectory(uploadsFolder);

                try
                {
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(fileStream);
                    }
                    // Construct the image URL (relative path)
                    imageUrl = Path.Combine("images", uniqueFileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error saving the image: {ex.Message}");
                }
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
                Scent = product.Scent,
                IsBestSeller = product.IsBestSeller,
                IsDailyOffer= product.IsDailyOffer,
                Name = product.Name,
                ImageUrl = imageUrl

            };
                
            await _productRepo.AddAsync(Addproduct);
            return Ok();


        }
        [HttpPost("add-productVariant/{id}")]
        public async Task<ActionResult> addProductVariants(List<AddProductVariantsDto> productVariants, int id)
        {
            var product = await  _productRepo.GetByIdAsync(id);
            if (product == null) return BadRequest("Product is null");
            var newVariants = productVariants.Select(variant => new ProductVariant
            {
                Barcode = variant.Barcode,
                Price = variant.Price,
                StockQuantity = variant.StockQuantity,
                Weight = variant.Weight,
                ProductId = id,
            }).ToList();

            await _productVariantRepo.AddRangeAsync(newVariants);
            return Ok("Product variant added successfully");
        }

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> updateProduct([FromForm] UpdateProduct product, int id)
        {
            var existingProduct = await _productRepo.GetByIdAsync(id);
            if (existingProduct == null) return NotFound("Product not found");

            if (product.Image is not null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;

                var filepath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await product.Image.CopyToAsync(filestream);
                }

                existingProduct.ImageUrl = Path.Combine("images", uniqueFileName);
            }

            existingProduct.Name = product.Name ;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Benfits = product.Benefits ?? existingProduct.Benfits;
            existingProduct.Features = product.Features ?? existingProduct.Features;
            existingProduct.CalltoAction = product.CallToAction ?? existingProduct.CalltoAction;
            existingProduct.DiscountPercentage = product.DiscountPercentage;
            existingProduct.Scent = product.Scent ;
            existingProduct.IsBestSeller = product.IsBestSeller ;
            existingProduct.IsDailyOffer = product.IsDailyOffer;

            await _productRepo.UpdateAsync(existingProduct);
            return Ok();
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
