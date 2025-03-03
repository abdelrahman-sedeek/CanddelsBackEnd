﻿using System.Linq.Expressions;

namespace CanddelsBackEnd.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } // where
        List<Expression<Func<T, object>>> Includes { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
       



    }
}
