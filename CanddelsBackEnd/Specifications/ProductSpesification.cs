using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class ProductSpesification : BaseSpecification<Product>
    {
        public ProductSpesification()
        {
            AddInclude(X => X.productVariants);
            AddInclude(X =>X.Category);
        }
        public ProductSpesification(int id):base(x=>x.Id==id)
        {
            AddInclude(X => X.productVariants);
            AddInclude(X => X.Category);
        }
    }
}
