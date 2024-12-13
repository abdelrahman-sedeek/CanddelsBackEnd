using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class ProductSpesification : BaseSpecification<Product>
    {
        public ProductSpesification()
        {
            AddInclude(X => X.productVariants);
            AddInclude(X =>X.Category);
            AddInclude(x => x.Discount);
        }
        public ProductSpesification(int id):base(x=>x.Id==id)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X => X.Category);
            AddInclude(X => X.Discount);

        }
        public ProductSpesification(bool IsBestSeller) :base(x=>x.IsBestSeller== IsBestSeller)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X => X.Category);
        }
    
    }
}
