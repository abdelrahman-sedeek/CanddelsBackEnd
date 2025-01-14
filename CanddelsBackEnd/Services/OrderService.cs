using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.CartRepo;
using CanddelsBackEnd.Repositories.OrderRepo;
using CanddelsBackEnd.Repositories.ShippingDetailsRepo;
using Microsoft.EntityFrameworkCore;

namespace CanddelsBackEnd.Services
{
    public class OrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingDetailsRepo _shippingDetailRepository;
        private readonly CandelContext _context;

        public OrderService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IShippingDetailsRepo shippingDetailRepository,
            CandelContext context)
        {
          
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _shippingDetailRepository = shippingDetailRepository;
            _context = context;
        }

        public async Task<Order> ConfirmOrderAsync(string sessionId, ShippingDetailsDto shippingDetail)
        {
            
            var cart = await _cartRepository.GetCartBySessionIdAsync(sessionId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty or does not exist.");
            }

            var existingDetail = await _context.ShippingDetails
        .SingleOrDefaultAsync(sd => sd.Email == shippingDetail.Email && sd.PhoneNumber == shippingDetail.PhoneNumber);

            ShippingDetail detail;
            if (existingDetail != null)
            {
                // Reuse the existing ShippingDetail
                detail = existingDetail;
            }
            else
            {
                // Create a new ShippingDetail
                detail = new ShippingDetail
                {
                    Address = shippingDetail.Address,
                    City = shippingDetail.City,
                    PostalCode = shippingDetail.PostalCode,
                    Country = shippingDetail.Country,
                    Email = shippingDetail.Email,
                    FullName = shippingDetail.FullName,
                    PhoneNumber = shippingDetail.PhoneNumber,
                    State = shippingDetail.State,
                };

                await _context.ShippingDetails.AddAsync(detail);
                await _context.SaveChangesAsync();
            }

            // Create the order
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                SubTotal = (decimal)cart.CartItems.Sum(ci =>
                {
                    var discountPercentage = ci.ProductVariant.Product.DiscountPercentage ?? 0;
                    var discountedPrice = discountPercentage > 0
                        ? ci.ProductVariant.Price - (ci.ProductVariant.Price * discountPercentage / 100)
                        : ci.ProductVariant.Price;

                    return discountedPrice * ci.Quantity;
                }),
                OrderStatus = "Pending",
                PaymentStatus = "Unpaid",
                ShippingDetailId = detail.Id,
                OrderItems = cart.CartItems.Select(ci =>
                {
                    var discountPercentage = ci.ProductVariant.Product.DiscountPercentage;
                    var discountedPrice = discountPercentage > 0
                        ? ci.ProductVariant.Price - (ci.ProductVariant.Price * discountPercentage / 100)
                        : ci.ProductVariant.Price;

                    return new OrderItem
                    {
                         productVariantId = (int)ci.ProductVariantId,
                        Quantity = ci.Quantity,
                        Total = (decimal)(discountedPrice * ci.Quantity) 
                    };
                }).ToList()
            };

            await _orderRepository.AddOrderAsync(order);

            
            _cartRepository.RemoveCartItems(cart.CartItems);
            _cartRepository.RemoveCart(cart);
 

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders
                .Include(o=>o.ShippingDetail)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.productVariant)
                .ThenInclude(pv=> pv.Product)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Order> GetOrderById(int id) 
            => await _context.Orders.Include(o=>o.OrderItems).ThenInclude(oi=>oi.productVariant).SingleOrDefaultAsync(o => o.Id == id);

        public async Task UpdateOrder(Order order)
        {
             _context.Orders.Update(order);
            await _context.SaveChangesAsync();

        }
    }
}
