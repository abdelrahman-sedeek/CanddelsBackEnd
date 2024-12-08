using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.PorductRepo
{
    public interface IproductRepository
    {
        Task<Product> GetProductByidAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();

    }
}
