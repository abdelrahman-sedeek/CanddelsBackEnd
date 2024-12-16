using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
namespace CanddelsBackEnd.Helper
{
    class LowesPriceDiscountResolver : IValueResolver<Product, ProductToReturnDto,decimal>
    {
        public  decimal Resolve(Product source, ProductToReturnDto destination, decimal destMember, ResolutionContext context)
        {
            if(source.Discount is null && source.productVariants.Count > 1)
            {
                return source.productVariants.Min(pv=>pv.Price);
            } 
            else if(source.Discount is null && source.productVariants.Count== 1)
            {
                return source.productVariants.Min(pv=>pv.Price);
            }
            else if (source.Discount is not null && source.productVariants.Count == 1)
            {
                return source.productVariants.Min(pv => pv.Price) - (source.productVariants.Min(pv => pv.Price) * (source.Discount.DiscountPercentage / 100));
            }
            else
            {
                return source.productVariants.Min(pv => pv.Price) -( source.productVariants.Min(pv=>pv.Price)*(source.Discount.DiscountPercentage/100));
            }
        }
    }
    class LowesPriceDiscountForProductDtoIdResolver : IValueResolver<Product, ProductToReturnByIdDto,decimal>
    {
        public  decimal Resolve(Product source, ProductToReturnByIdDto destination, decimal destMember, ResolutionContext context)
        {
            if(source.Discount is null && source.productVariants.Count > 1)
            {
                return source.productVariants.Min(pv=>pv.Price);
            } 
            else if(source.Discount is null && source.productVariants.Count== 1)
            {
                return source.productVariants.Min(pv=>pv.Price);
            }
            else if (source.Discount is not null && source.productVariants.Count == 1)
            {
                return source.productVariants.Min(pv => pv.Price) - (source.productVariants.Min(pv => pv.Price) * (source.Discount.DiscountPercentage / 100));
            }
            else
            {
                return source.productVariants.Min(pv => pv.Price) -( source.productVariants.Min(pv=>pv.Price)*(source.Discount.DiscountPercentage/100));
            }
        }
    }
}
