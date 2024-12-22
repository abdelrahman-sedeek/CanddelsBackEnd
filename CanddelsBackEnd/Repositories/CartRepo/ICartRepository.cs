using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.CartRepo
{
    public interface ICartRepository
    {
        Task<Cart> GetCartBySessionIdAsync(string sessionId);
        Task AddCartAsync(Cart cart);
        Task SaveChangesAsync();
        void RemoveCart(Cart cart);
        void RemoveCartItems(ICollection<CartItem> cartItems);
        Task<Cart> GetCartWithItemsAsync(int cartId);
    }
}
