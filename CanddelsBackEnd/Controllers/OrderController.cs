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
        public async Task<IActionResult> ConfirmOrder([FromBody] ShippingDetailsDto shippingDetail)
        {
            if(!Request.Cookies.TryGetValue("SessionId",out var sessionIdValue))
            {
                return BadRequest(new { Error = "SessionId header is missing or invalid." });
            }

            if (shippingDetail == null)
            {
                return BadRequest(new { Error = "Shipping details are required." });
            }

            var sessionId = sessionIdValue.ToString();
            var order= await _orderService.ConfirmOrderAsync(sessionId, shippingDetail);
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
