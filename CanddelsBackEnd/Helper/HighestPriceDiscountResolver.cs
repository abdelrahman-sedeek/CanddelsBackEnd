using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
namespace CanddelsBackEnd.Helper
{
    class HighestPriceDiscountPercentageResolver : IValueResolver<Product, ProductToReturnDto, decimal>
    {
        public decimal Resolve(Product source, ProductToReturnDto destination, decimal destMember, ResolutionContext context)
        {
            if (source.productVariants == null || !source.productVariants.Any())
                return 0; // or another default value

            var maxPrice = source.productVariants.Max(pv => pv.Price);

            if (source.DiscountPercentage is null)
                return maxPrice;

            return maxPrice - (maxPrice * (source.DiscountPercentage.Value / 100));
        }
    }

}
