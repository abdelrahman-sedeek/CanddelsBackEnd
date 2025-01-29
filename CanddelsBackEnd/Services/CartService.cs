using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.CartRepo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CanddelsBackEnd.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly CandelContext _context;

        public CartService(ICartRepository cartRepository,CandelContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }

        public async Task<Cart> GetCartBySessionIdAsync(string sessionId)
        {
            return await _cartRepository.GetCartBySessionIdAsync(sessionId);
        }

        public async Task<Cart> GetOrCreateCartAsync(string sessionId)
        {
            var cart = await _cartRepository.GetCartBySessionIdAsync(sessionId);
            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.Add(Cart.ExpirationDuration)
                };
                await _cartRepository.AddCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }
            return cart;
        }

        public async Task<string> AddToCartAsync(AddToCartRequest request)
        {
            var cart = await GetOrCreateCartAsync(request.SessionId);

          
                var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductVariantId == request.ProductVariantId);
                if (cartItem != null)
                {
                    cartItem.Quantity += request.Quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductVariantId = request.ProductVariantId,
                        Quantity = request.Quantity,
                        CartId = cart.Id
                    });

                }
         
              await _cartRepository.SaveChangesAsync();
            return "Product added to cart successfully";
        }
        public async Task<string> AddCustomProductToCartAsync(AddCustomProductToCartDto request)
        {

            if(request.ScentIds is null || !request.ScentIds.Any())
            {
                throw new ValidationException("At least one scent must be selected.");
            }

            var cart = await GetOrCreateCartAsync(request.SessionId);
            var customProduct = new CustomProduct
            {
                Weight = request.Weight ?? 0,

                CustomProductScents = request.ScentIds.Select(scentId => new CustomProductScent
                {
                    ScentId = scentId
                }).ToList()

            };
            _context.customProducts.AddAsync(customProduct);

            await _context.SaveChangesAsync();
            Console.WriteLine($"Custom product added with ID: {customProduct.Id}");

            cart.CartItems.Add(new CartItem
            {
                CustomProductId = customProduct.Id,
                Quantity = request.Quantity,
                CartId = cart.Id
            });
            await _cartRepository.SaveChangesAsync();
            return " custom Product added to cart successfully";
        }

        public async Task<string> UpdateQuantityAsync(UpdateCartQuantityDto dto)
        {
            if (!dto.IsValid())
                throw new ValidationException("Must provide either ProductVariantId or CustomProductId");

            var cart = await _cartRepository.GetCartBySessionIdAsync(dto.SessionId);
            if (cart == null) return "Cart not found";

            CartItem cartItem = null;

            if (dto.ProductVariantId.HasValue)
            {
                cartItem = cart.CartItems.SingleOrDefault(ci =>
                    ci.ProductVariantId == dto.ProductVariantId.Value);
            }
            else if (dto.CustomProductId.HasValue)
            {
                cartItem = cart.CartItems.SingleOrDefault(ci =>
                    ci.CustomProductId == dto.CustomProductId.Value);
            }

            if (cartItem == null) return "Cart item not found";

            cartItem.Quantity = dto.Quantity;
            await _cartRepository.SaveChangesAsync();

            return $"{(dto.ProductVariantId.HasValue ? "Product" : "Custom product")} quantity updated";
        }
        public async Task<string> RemoveFromCartAsync(RemoveFromCartRequest dto)
        {
            if (!dto.IsValid())
                throw new ValidationException("Must provide either ProductVariantId or CustomProductId");

            var cart = await _cartRepository.GetCartBySessionIdAsync(dto.SessionId);
            if (cart == null) return "Cart not found";

            CartItem cartItem = null;

            if (dto.ProductVariantId.HasValue)
            {
                cartItem = cart.CartItems.SingleOrDefault(ci =>
                    ci.ProductVariantId == dto.ProductVariantId.Value);
            }
            else if (dto.CustomProductId.HasValue)
            {
                cartItem = cart.CartItems.SingleOrDefault(ci =>
                    ci.CustomProductId == dto.CustomProductId.Value);
            }

            if (cartItem == null) return "Cart item not found";

            // Remove cart item
            cart.CartItems.Remove(cartItem);
            await _cartRepository.SaveChangesAsync();

            // Clean up orphaned custom product
            if (dto.CustomProductId.HasValue)
            {
                var customProduct = await _context.customProducts
                    .FindAsync(dto.CustomProductId.Value);
                if (customProduct != null)
                {
                    _context.customProducts.Remove(customProduct);
                    await _context.SaveChangesAsync();
                }
            }

            return $"{(dto.ProductVariantId.HasValue ? "Product" : "Custom product")} removed from cart";
        }
        public async Task<IEnumerable<object>> ViewCartAsync(string sessionId)
        {
            var cart = await _cartRepository.GetCartBySessionIdAsync(sessionId);
            if (cart == null || !cart.CartItems.Any()) return null;

            return cart.CartItems.Select(ci =>
            {
                if (ci.ProductVariant != null)
                {
                    var discount = ci.ProductVariant.Product.DiscountPercentage;
                    bool isOfferActive = ci.ProductVariant.Product.IsDailyOffer && ci.ProductVariant.Product.DiscountPercentage !=null;

                    return new
                    {   ci.Id,
                        ci.ProductVariantId,
                        name = ci.ProductVariant.Product.Name,
                        ci.ProductVariant.Product.ImageUrl,
                        ci.ProductVariant.Product.Scent,
                        ci.Quantity,
                        Price = isOfferActive
                            ? ci.ProductVariant.Price - (discount * ci.ProductVariant.Price / 100)
                            : ci.ProductVariant.Price
                    } as object;


                }
                else if (ci.CustomProduct != null)
                {
                    return new
                    {
                        ci.CustomProductId,
                        ci.CustomProduct.Weight,
                        ci.Quantity,
                        Scents = ci.CustomProduct.CustomProductScents
                        .Select(cps => new
                        {
                            cps.Scent.Id,
                            cps.Scent.Name,
                            cps.Scent.Description,
                            cps.Scent.ImageUrl
                        })                  

                    } as object;
                }
                else
                    return null;
               
           
           
            });
            
        }
    }

}
