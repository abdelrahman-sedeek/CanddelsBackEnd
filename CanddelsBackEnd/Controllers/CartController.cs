using Azure.Core;
using Azure;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

[ApiController]
[Route("api/cart")]

public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

<<<<<<< Updated upstream
        public CartController(CandelContext candelContext)
=======
    [HttpGet("create-session")]
    public async Task<IActionResult> CreateSession()
    {
        if (Request.Cookies.TryGetValue("SessionId", out var existingSessionId))
>>>>>>> Stashed changes
        {
            var Getcart = await _cartService.GetCartBySessionIdAsync(existingSessionId);
            if (Getcart != null)
            {
                return Ok(new { sessionId = existingSessionId });
            }
        }

        var sessionId = GenerateSecureSessionId();
        var cart = await _cartService.GetOrCreateCartAsync(sessionId);

        Response.Cookies.Append("SessionId", sessionId, new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(2)
        });

        return Ok(new { sessionId });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest addToCartRequest)
    {
        var result = await _cartService.AddToCartAsync(addToCartRequest);
        return Ok(new { message = result });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest removeFromCartRequest)
    {
        var result = await _cartService.RemoveFromCartAsync(removeFromCartRequest);
        if (result == null)
        {
            return NotFound("Cart or item not found");
        }
        return Ok(new { message = result });
    }

<<<<<<< Updated upstream
            if(cart is null)
            {
                cart = new Cart
                {
                    SessionId = sessionId
                };
                await _candelContext.Carts.AddAsync(cart);
                await _candelContext.SaveChangesAsync();
            }


            Response.Cookies.Append("SessionId", sessionId, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return Ok(new { sessionId= sessionId });
=======
    [HttpGet("view")]
    public async Task<IActionResult> ViewCart()
    {
        if (!Request.Headers.TryGetValue("SessionId", out var sessionIdValues))
        {
            return BadRequest(new { message = "SessionId is missing" });
>>>>>>> Stashed changes
        }

        var sessionId = sessionIdValues.ToString();
        var cartItems = await _cartService.ViewCartAsync(sessionId);

        if (cartItems == null || !cartItems.Any())
        {
            return Ok(new { message = "Cart is empty" });
        }

        return Ok(cartItems);
    }

    private string GenerateSecureSessionId()
    {
        return Guid.NewGuid().ToString("N") + "-" + RandomNumberGenerator.GetInt32(1000, 10000);
    }
}

