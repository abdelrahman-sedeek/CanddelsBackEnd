using Azure.Core;
using Azure;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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


            Response.Cookies.Append("SessionId", sessionId, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });


            return Ok(new { sessionId= sessionId });

    [HttpGet("view")]
    public async Task<IActionResult> ViewCart()
    {
        if (!Request.Headers.TryGetValue("SessionId", out var sessionIdValues))

            return Ok(new { 
                sessionId= sessionId,
            });
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest addToCartRequest)
        {
            var cart = await _candelContext.Carts.Include(c=>c.CartItems).SingleOrDefaultAsync(c=>c.SessionId == addToCartRequest.SessionId);

            if(cart is null)
            {   
                cart = new Cart { SessionId = addToCartRequest.SessionId };
                _candelContext.Carts.Add(cart);
                await _candelContext.SaveChangesAsync();
            }

            var cartItem =  cart.CartItems
                .SingleOrDefault(ci => ci.ProductVariantId == addToCartRequest.ProductVariantId);

            if(cartItem is not null)
            {
                cartItem.Quantity += addToCartRequest.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductVariantId = addToCartRequest.ProductVariantId,
                    Quantity = addToCartRequest.Quantity,
                    CartId = cart.Id
                });
            }

            await _candelContext.SaveChangesAsync();

            return Ok(new { message = "Product added to cart successfully" });

        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest removeFromCartRequest)


        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartQuantityDto updateCartQuantityDto)
        {
            var cart = await _candelContext.Carts
                .Include(c => c.CartItems)
                .SingleOrDefaultAsync(c => c.SessionId == updateCartQuantityDto.SessionId);

            var cartItem = cart?.CartItems.SingleOrDefault(ci => ci.ProductVariantId == updateCartQuantityDto.ProductVariantId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            cartItem.Quantity =updateCartQuantityDto.Quantity;
            await _candelContext.SaveChangesAsync();

            return Ok("Quantity updated successfully");

        }

        [HttpGet("view")]
        public async Task<IActionResult> ViewCart()
        {
            return BadRequest(new { message = "SessionId is missing" });

            if(!Request.Headers.TryGetValue("SessionId", out var sessionIdValues))
            {
                return BadRequest(new
                {
                    message = "SessionId is missing"
                });
            }
            var sessionId = sessionIdValues.ToString();
            var cart  = await _candelContext.Carts
                .Include(c=>c.CartItems)
                .ThenInclude(ci=>ci.ProductVariant)
                .ThenInclude(pv=>pv.Product)
                .ThenInclude(p=>p.Discount)
                .SingleOrDefaultAsync(c=>c.SessionId==sessionId);

            

            if(cart is null || !cart.CartItems.Any())
            {
                return NotFound(new
                {
                    message = "Your cart is empty"
                });
            }

            var cartItems = cart.CartItems.Select(ci =>
            {
                var discount = ci.ProductVariant.Product.Discount;

                bool isOfferActive = discount != null && discount.StartDate <= DateTime.Now && discount.EndDate > DateTime.Now;

                return new
                {
                    ci.CartId,
                    ci.ProductVariantId,
                    name = ci.ProductVariant.Product.Name,
                    weight = ci.ProductVariant.Weight,
                    ci.ProductVariant.Product.ImageUrl,
                    ci.ProductVariant.Product.Scent,
                    ci.Quantity,
                    Price = isOfferActive ? 
                    ci.ProductVariant.Price - (ci.ProductVariant.Product.Discount?.DiscountPercentage * ci.ProductVariant.Price / 100)
                    : ci.ProductVariant.Price
                    
                };
            });
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

