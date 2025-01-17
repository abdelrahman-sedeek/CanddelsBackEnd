using AutoMapper;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Services;
using CanddelsBackEnd.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{

    [ApiController]
        [Route("api/[controller]")]
    public class OrderController : ControllerBase    
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _repo;

        public OrderController(OrderService orderService,IMapper mapper,IGenericRepository<Order> repo)
        {
            _orderService = orderService;
            _mapper = mapper;
            _repo = repo;
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
    
        [HttpPut("update-order/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto updateOrder)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
                return NotFound("order not found");

            order.OrderStatus = updateOrder.OrderStatus;
            order.PaymentStatus = updateOrder.PaymentStatus;

            await _orderService.UpdateOrder(order);

            return Ok();

        }

        [HttpGet("/dashboard/orders")]
        public async Task<IActionResult> showOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }
        [HttpGet("/dashboard/orders/{id}")]
        public async Task<IActionResult> showOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }


    }
}
