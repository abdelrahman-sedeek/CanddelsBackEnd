using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CanddelsBackEnd.Helper
{
    public class CartHelper
    {

            private readonly CandelContext _context;

            public CartHelper(CandelContext context)
            {
                _context = context;
            }

            public async Task<Cart> CreateSessionAsync()
            {
                string sessionId = Guid.NewGuid().ToString(); 
                var cart = new Cart
                {
                    SessionId = sessionId,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.Add(Cart.ExpirationDuration)
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart;
            }

            private async Task DeleteCartAsync(Cart cart)
            {
                _context.CartItems.RemoveRange(cart.CartItems); 
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
            public async Task<Cart> AddItemToCartAsync(string sessionId, int productVariantId, int quantity)
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.SessionId == sessionId);

                if (cart == null || DateTime.UtcNow > cart.ExpiresAt)
                {
                     if (cart != null)
                    {
                        await DeleteCartAsync(cart);
                    }

                    cart = await CreateSessionAsync();
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductVariantId == productVariantId);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        ProductVariantId = productVariantId,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
                await _context.SaveChangesAsync();
                return cart;
            }
        }

    }

