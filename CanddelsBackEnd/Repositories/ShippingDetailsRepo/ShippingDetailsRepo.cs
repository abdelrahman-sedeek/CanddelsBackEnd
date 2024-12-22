using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.ShippingDetailsRepo
{
    public class ShippingDetailsRepo:IShippingDetailsRepo
    {
        private readonly CandelContext _context;

        public ShippingDetailsRepo(CandelContext context)
        {
            _context = context;
        }

        //public async Task AddShippingDetailAsync(ShippingDetailsDto shippingDetail)
        //{
           
        //    await _context.ShippingDetails.AddAsync(shippingDetail);
        //}
    }
}
