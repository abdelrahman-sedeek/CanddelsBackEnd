using CanddelsBackEnd.Helper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartHelper _cartService;

        public CartController(CartHelper cartHelper)
        {
            _cartService = cartHelper;
        }

        [HttpPost("generate-session")]
        public async Task<IActionResult> GenerateSession()
        {
            var session =  await _cartService.CreateSessionAsync();
            return Ok(new
            {
                sessionId = session.SessionId,
                ExpiredAt =session.ExpiresAt
            });
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(string sessionId, int productVariantId, int quantity)
        {
            try
            {
                var cart = await _cartService.AddItemToCartAsync(sessionId, productVariantId, quantity);
                return Ok(new
                {
                    Message = "Item added to cart successfully.",
                    NewSessionId = cart.SessionId,
                    ExpiresAt = cart.ExpiresAt
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
