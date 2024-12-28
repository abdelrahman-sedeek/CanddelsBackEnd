using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/cart")]

public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }


    [HttpGet("create-session")]
    public async Task<IActionResult> CreateSession()
    {
        if (Request.Cookies.TryGetValue("SessionId", out var existingSessionId))

        {
            var Getcart = await _cartService.GetCartBySessionIdAsync(existingSessionId);
            if (Getcart != null)
            {
                return Ok(new { sessionId = existingSessionId });
            }
        }

            var sessionId = GenerateSecureSessionId();

            var cart = await _candelContext.Carts
                .SingleOrDefaultAsync(c => c.SessionId == sessionId);

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

            if(cart is null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.Add(Cart.ExpirationDuration)
                };
                await _candelContext.Carts.AddAsync(cart);
                await _candelContext.SaveChangesAsync();
            }

    [HttpPut("update-quantity")]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartQuantityDto updateCartQuantityDto)
    {
        var cart = await _candelContext.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.SessionId == updateCartQuantityDto.SessionId);


            return Ok(new { sessionId= sessionId });

    [HttpGet("view")]
    public async Task<IActionResult> ViewCart()
    {
        if (!Request.Headers.TryGetValue("SessionId", out var sessionIdValues))

            return Ok(new { 
                sessionId= sessionId,
            });
        }

        cartItem.Quantity = updateCartQuantityDto.Quantity;
        await _candelContext.SaveChangesAsync();

        return Ok("Quantity updated successfully");

    }

    [HttpGet("view")]
    public async Task<IActionResult> ViewCart()
    {
        if (!Request.Headers.TryGetValue("SessionId", out var sessionIdValues))
        {
            return BadRequest(new
            {
                message = "SessionId is missing"
            });
        }

            await _candelContext.SaveChangesAsync();

            return Ok(new { message = "Product added to cart successfully" });

        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest removeFromCartRequest)

        {
            return BadRequest(new { message = "SessionId is missing" });

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

