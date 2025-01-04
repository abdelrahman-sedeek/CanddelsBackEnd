using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
namespace CanddelsBackEnd.Specifications

{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery,
            ISpecification<T> spec)
        {
            var query = InputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);

            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
          
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            
            return query;



        }
    }
}
