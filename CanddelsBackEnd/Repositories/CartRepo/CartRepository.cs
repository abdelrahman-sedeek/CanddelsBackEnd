namespace CanddelsBackEnd.Repositories.CartRepo
{
    public class CartRepository
    {

<<<<<<< Updated upstream
=======
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
>>>>>>> Stashed changes
    }
}
