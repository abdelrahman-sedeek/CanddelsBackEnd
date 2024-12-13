using static System.Runtime.InteropServices.JavaScript.JSType;
using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
using System.Runtime.CompilerServices;
namespace CanddelsBackEnd.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductVariant, ProductVariantDto>();
          
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.HighestPrice, opt => opt.MapFrom(src => GetHighestPrice(src.productVariants)))
                .ForMember(dest => dest.LowestPrice, opt => opt.MapFrom(src => GetLowestPrice(src.productVariants)))
                .ForMember(dest => dest.HighestPriceAfterDiscount, opt => opt.MapFrom<HighestPriceDiscountResolver>())
                .ForMember(dest => dest.LowestPriceAfterDiscount, opt => opt.MapFrom<LowesPriceDiscountResolver>());
                    

            CreateMap<Product, ProductToReturnByIdDto>()
                .ForMember(dest => dest.HighestPriceAfterDiscount, opt => opt.MapFrom(src => GetHighestPrice(src.productVariants)))
                .ForMember(dest => dest.LowestPriceAfterDiscount, opt => opt.MapFrom(src => GetLowestPrice(src.productVariants)));
       
            CreateMap<Category, CategoryToReturnDto>();
        }
        private decimal? GetHighestPrice(IEnumerable<ProductVariant> variants) =>
                variants != null && variants.Any() 
                    ? variants.Max(v => v.Price)
                    : null;
        private decimal? GetLowestPrice(IEnumerable<ProductVariant> variants) =>
            variants != null && variants.Any() 
                ? variants.Min(v => v.Price)
                : null;
        
    }
}
