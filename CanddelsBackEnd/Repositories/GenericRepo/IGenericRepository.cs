using CanddelsBackEnd.Models;
using CanddelsBackEnd.Specifications;

namespace CanddelsBackEnd.Repositories.GenericRepo
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <List<T>> GetAllAsync();
        Task <T> GetByIdAsync(int Id);
        Task <List<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task <T> GetByIdWithSpecAsync(ISpecification<T> spec);


        
    }
}
