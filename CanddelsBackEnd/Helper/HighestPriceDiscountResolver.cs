using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
namespace CanddelsBackEnd.Helper
{
    class HighestPriceDiscountPercentageResolver : IValueResolver<Product, ProductToReturnDto,decimal>
    {
        public  decimal Resolve(Product source, ProductToReturnDto destination, decimal destMember, ResolutionContext context)
        {

            if(source.DiscountPercentage is null && source.productVariants.Count > 1)
            {
                return source.productVariants.Max(pv=>pv.Price);
            } 
            else if(source.DiscountPercentage is null && source.productVariants.Count == 1)
            {
                return source.productVariants.Max(pv=>pv.Price);
            }
            else if(source.DiscountPercentage is not null && source.productVariants.Count == 1)
            {
                return (decimal)(source.productVariants.Max(pv => pv.Price) - (source.productVariants.Max(pv => pv.Price) * (source.DiscountPercentage / 100)));
            }
            else
            {
                return (decimal)(source.productVariants.Max(pv => pv.Price)-(source.productVariants.Max(pv=>pv.Price)*(source.DiscountPercentage/100)));
            }
        }
    } 

  
}
