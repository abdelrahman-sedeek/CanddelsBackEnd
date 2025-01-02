using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
namespace CanddelsBackEnd.Helper
{
    class LowesPriceDiscountPercentageResolver : IValueResolver<Product, ProductToReturnDto, decimal>
    {
        public decimal Resolve(Product source, ProductToReturnDto destination, decimal destMember, ResolutionContext context)
        {
            if (source.productVariants == null || !source.productVariants.Any())
                return 0; // or another default value

            var minPrice = source.productVariants.Min(pv => pv.Price);

            if (source.DiscountPercentage is null)
                return minPrice;

            return minPrice - (minPrice * (source.DiscountPercentage.Value / 100));
        }
    }


}
