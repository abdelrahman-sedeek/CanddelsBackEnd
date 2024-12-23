using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CandelContext _context;

        public OrderRepository(CandelContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
    }
}
