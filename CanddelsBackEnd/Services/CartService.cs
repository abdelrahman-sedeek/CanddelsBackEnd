using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.CartRepo;

namespace CanddelsBackEnd.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
<<<<<<< HEAD
                var discount = ci.ProductVariant.Product.DiscountPercentage;
                bool isOfferActive = ci.ProductVariant.Product.IsDailyOffer && ci.ProductVariant.Product.DiscountPercentage !=null;
=======
                var discount = ci.ProductVariant.Product.Discount;
                bool isOfferActive = discount != null && discount.StartDate <= DateTime.Now && discount.EndDate > DateTime.Now;
>>>>>>> 433c6e7619e53375c895c1ce2484b0640f0af728

                return new
                {   ci.Id,
                    ci.ProductVariantId,
                    name = ci.ProductVariant.Product.Name,
                    ci.ProductVariant.Product.ImageUrl,
                    ci.ProductVariant.Product.Scent,
                    ci.Quantity,
                    Price = isOfferActive
<<<<<<< HEAD
                        ? ci.ProductVariant.Price - (discount* ci.ProductVariant.Price / 100)
=======
                        ? ci.ProductVariant.Price - (discount?.DiscountPercentage * ci.ProductVariant.Price / 100)
>>>>>>> 433c6e7619e53375c895c1ce2484b0640f0af728
                        : ci.ProductVariant.Price
                };
            });
        }
    }

}
