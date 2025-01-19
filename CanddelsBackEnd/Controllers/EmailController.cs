using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] EmailRequest emailRequest)
        {
            if (string.IsNullOrEmpty(emailRequest.Email))
            {
                return BadRequest("Email is required.");
            }

            await _emailService.SendVerificationEmail(emailRequest.Email);
            return Ok();
        }

        [HttpPost("verify-token")]
        public IActionResult VerifyToken()
        {
            // Access Authorization header
            var token = Request.Headers["Authorization"].FirstOrDefault();

            // Check if the header is missing
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Authorization header is missing.");
            }

            // Check if it starts with "Bearer "
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            // Validate the token
            if (!_emailService.VerifyToken(token))
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok();
        }



    }
}
