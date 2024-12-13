using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class ProductSpesification : BaseSpecification<Product>
    {
        //public ProductSpesification()
        //{
        //    AddInclude(X => X.productVariants);
        //    AddInclude(X => X.Category);
        //}
        public ProductSpesification(ProductParameters ProductPrams)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X =>X.Category);
            ApplyPaging(ProductPrams.PageSize * (ProductPrams.PageIndex - 1), ProductPrams.PageSize);
        }
        public ProductSpesification(int id):base(x=>x.Id==id)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X => X.Category);
        }
        public ProductSpesification(bool IsBestSeller) :base(x=>x.IsBestSeller== IsBestSeller)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X => X.Category);
        }
    
    }
}
