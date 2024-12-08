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
                .ForMember(dest => dest.HighestPrice,
                    opt => opt.MapFrom(src =>
                        src.productVariants != null && src.productVariants.Any() && src.productVariants.Count() > 1
                            ? src.productVariants.Max(v => v.Price)
                            : (decimal?)null))
                .ForMember(dest => dest.LowestPrice,
                    opt => opt.MapFrom(src =>
                        src.productVariants != null && src.productVariants.Any()
                            ? src.productVariants.Min(v => v.Price)
                            : (decimal?)null));
                  
 


        }
    }
}
