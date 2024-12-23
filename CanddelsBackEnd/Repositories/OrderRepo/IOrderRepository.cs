using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.OrderRepo
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }

    
}
