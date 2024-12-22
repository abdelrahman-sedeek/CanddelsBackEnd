using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.CartRepo;
using CanddelsBackEnd.Repositories.OrderRepo;
using CanddelsBackEnd.Repositories.ShippingDetailsRepo;

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

        public async Task<Order> ConfirmOrderAsync(int cartId, ShippingDetailsDto shippingDetail)
        {
            
            var cart = await _cartRepository.GetCartWithItemsAsync(cartId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty or does not exist.");
            }

            var detail = new ShippingDetail
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
            // Create the order
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                SubTotal = cart.CartItems.Sum(ci => ci.ProductVariant.Price * ci.Quantity),
                OrderStatus = "Pending", 
                PaymentStatus = "Unpaid", 
                ShippingDetailId = detail.Id,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    productVariantId = ci.ProductVariantId,
                    Quantity = ci.Quantity,
                    Total = ci.ProductVariant.Price * ci.Quantity
                }).ToList()
            };

            await _orderRepository.AddOrderAsync(order);

            
            _cartRepository.RemoveCartItems(cart.CartItems);
            _cartRepository.RemoveCart(cart);
 

            return order;
        }
    }
}
