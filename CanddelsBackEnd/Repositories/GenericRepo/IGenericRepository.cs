﻿using CanddelsBackEnd.Models;
using CanddelsBackEnd.Specifications;
using System.Linq.Expressions;

namespace CanddelsBackEnd.Repositories.GenericRepo
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <List<T>> GetAllAsync();
        Task <T> GetByIdAsync(int Id);
        Task <List<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task <T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddRangeAsync(IEnumerable<ProductVariant> entities);
        Task DeleteAsync(int id);
        Task UpdateRangeAsync(IEnumerable<ProductVariant> variants);
        Task<List<ProductVariant>> GetByProductIdAsync(int id);

        Task DeleteRangeAsync(IEnumerable<ProductVariant> variants);




    }
}
