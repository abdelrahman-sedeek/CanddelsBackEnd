using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanddelsBackEnd.Repositories.PorductRepo
{
    public class ProductRepository : IproductRepository

    {
        private readonly CandelContext _context;

        public ProductRepository(CandelContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]

        public async Task<Product> GetProductByidAsync(int id)
        {
            return await _context.Products.Include(pv => pv.productVariants).
                Include(c=>c.Category).
                FirstOrDefaultAsync();
        }

        public Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
