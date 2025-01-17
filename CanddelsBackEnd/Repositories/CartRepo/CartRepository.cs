using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CanddelsBackEnd.Repositories.CartRepo
{
    public class CartRepository:ICartRepository
    {
        private readonly CandelContext _context;

        public CartRepository(CandelContext context)
        {
            _context = context;

        }

        public async Task<Cart> GetCartBySessionIdAsync(string sessionId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                     .ThenInclude(ci => ci.ProductVariant)
                     .ThenInclude(pv => pv.Product)
                .Include(cp=>cp.CartItems)
                    .ThenInclude(cp=>cp.CustomProduct)
                .SingleOrDefaultAsync(c => c.SessionId == sessionId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }
        public async Task<Cart> GetCartWithItemsAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public void RemoveCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void RemoveCartItems(ICollection<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
