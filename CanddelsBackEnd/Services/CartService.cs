using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.CartRepo;
using Microsoft.EntityFrameworkCore;

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
            var cart = await GetOrCreateCartAsync(request.SessionId);
            var customProduct = new CustomProduct
            {
                Scent1 = request.Scent1,
                Scent2 = request.Scent2,
                Scent3 = request.Scent3,
                Scent4 = request.Scent4,  
                Weight = request.Weight ?? 0
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
        public async Task<string?> RemoveFromCartAsync(RemoveFromCartRequest request)
        {
            var cart = await _cartRepository.GetCartBySessionIdAsync(request.SessionId);
            if (cart == null) return null;

            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductVariantId == request.ProductVariantId);
            if (cartItem == null) return null;

            cart.CartItems.Remove(cartItem);
            await _cartRepository.SaveChangesAsync();

            return "Product removed from cart";
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
                        ci.CustomProduct.Scent1,
                        ci.CustomProduct.Scent2,
                        ci.CustomProduct.Scent3,
                        ci.CustomProduct.Scent4,
                        ci.CustomProduct.Weight,


                    } as object;
                }
                else
                    return null;
               
           
           
            });
            
        }
    }

}
