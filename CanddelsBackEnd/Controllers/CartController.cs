using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using CanddelsBackEnd.Contexts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/cart")]

public class CartController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly CandelContext _candelContext;

    public CartController(CartService cartService, CandelContext candelContext)
    {
        _cartService = cartService;
        _candelContext = candelContext;
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
        if (addToCartRequest.ProductVariantId != null)
        {
            var result = await _cartService.AddToCartAsync(addToCartRequest);
            return Ok(new { message = result });
        }
        else
        {
            return BadRequest(new { message = "ProductVariantId is required for normal products." });
        }
    }
    [HttpPost("add-custom")]
    public async Task<IActionResult> AddCustomToCart([FromBody] AddCustomProductToCartDto addToCartRequest)
    {

        if (addToCartRequest == null)
        {
            return BadRequest("custom product is null ");

        }  
            var result = await _cartService.AddCustomProductToCartAsync(addToCartRequest);
            return Ok(new { message = result });
      
    }
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest dto)
    {
        if (!dto.IsValid())
            return BadRequest("Must provide either ProductVariantId or CustomProductId");

        try
        {
            var result = await _cartService.RemoveFromCartAsync(dto);
            return result switch
            {
                "Cart not found" or "Cart item not found" => NotFound(result),
                _ => Ok(new { message = result })
            };
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("update-quantity")]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartQuantityDto dto)
    {
        if (!dto.IsValid())
            return BadRequest("Must provide either ProductVariantId or CustomProductId");

        try
        {
            var result = await _cartService.UpdateQuantityAsync(dto);
            return result switch
            {
                "Cart not found" or "Cart item not found" => NotFound(result),
                _ => Ok(new { message = result })
            };
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
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

        var sessionId = sessionIdValues.ToString();

        var cartItems = await _cartService.ViewCartAsync(sessionId);

        if (cartItems == null || !cartItems.Any())
        {
            return BadRequest(new { message = "Cart is empty" });
        }

        return Ok(cartItems);
    }
    private string GenerateSecureSessionId()
    {
        return Guid.NewGuid().ToString("N") + "-" + RandomNumberGenerator.GetInt32(1000, 10000);
    }
}