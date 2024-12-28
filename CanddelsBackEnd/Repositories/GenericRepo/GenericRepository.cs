using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CanddelsBackEnd.Repositories.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CandelContext _context;

        public GenericRepository(CandelContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);

        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<ProductVariant> entities)
        {
            _context.ProductVariants.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException("Entity not found");
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return ApplySepecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySepecification(spec).FirstOrDefaultAsync();
        }

       
        private IQueryable<T> ApplySepecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
