using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Helper
{
    public class CandleUrl : IValueResolver<Product,ProductToReturnDto,string>
    {
        private readonly IConfiguration _configuration;

        public CandleUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrl))
            {
                return _configuration["ApiUrl"]+source.ImageUrl;

            }
            return source.ImageUrl;
        }
    }
}
