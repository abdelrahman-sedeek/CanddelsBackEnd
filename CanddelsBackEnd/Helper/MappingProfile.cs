using static System.Runtime.InteropServices.JavaScript.JSType;
using CanddelsBackEnd.Models;
using AutoMapper;
using CanddelsBackEnd.Dtos;
namespace CanddelsBackEnd.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductVariant, ProductVariantDto>();
          
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.HighestPrice, opt => opt.MapFrom(src => GetHighestPrice(src.productVariants)))
                .ForMember(dest => dest.LowestPrice, opt => opt.MapFrom(src => GetLowestPrice(src.productVariants)));

            CreateMap<Product, ProductToReturnByIdDto>()
                .ForMember(dest => dest.HighestPrice, opt => opt.MapFrom(src => GetHighestPrice(src.productVariants)))
                .ForMember(dest => dest.LowestPrice, opt => opt.MapFrom(src => GetLowestPrice(src.productVariants)));
       
            CreateMap<Category, CategoryToReturnDto>();
        }
        private decimal? GetHighestPrice(IEnumerable<ProductVariant> variants) =>
                variants != null && variants.Any() && variants.Count() > 1
                    ? variants.Max(v => v.Price)
                    : (decimal?)null;
        private decimal? GetLowestPrice(IEnumerable<ProductVariant> variants) =>
            variants != null && variants.Any()
                ? variants.Min(v => v.Price)
                : (decimal?)null;
       
    }
}
