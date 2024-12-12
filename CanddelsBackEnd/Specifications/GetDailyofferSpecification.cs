using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class GetDailyofferSpecification:BaseSpecification<Product>
    {
        public GetDailyofferSpecification(bool IsDailyOffer ) : base(x => x.IsDailyOffer== IsDailyOffer)
        {
            AddInclude(x => x.productVariants);
            AddInclude(x => x.Category);
        }
    }
}
