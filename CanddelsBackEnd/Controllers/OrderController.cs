using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{

        [ApiController]
        [Route("api/[controller]")]
    public class OrderController : ControllerBase    
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(OrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("confirm-order")]
        public async Task<IActionResult> ConfirmOrder( [FromHeader] int cartId, [FromBody] ShippingDetailsDto shippingDetail)
        {
            if(shippingDetail == null)
            {
                return BadRequest(new { Error = "Shipping details are required." });
            }
            var order= await _orderService.ConfirmOrderAsync(cartId, shippingDetail);
            return Ok(new
            {
                orderId=order.Id,
                orderDate=order.OrderDate,
                subtotal=order.SubTotal,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus
            });
        }

    }
}
