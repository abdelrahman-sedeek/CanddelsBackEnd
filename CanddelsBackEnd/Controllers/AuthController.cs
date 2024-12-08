using Azure.Core;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly AdminCredentialsManager _adminCredentialsManager;
        private readonly IPasswordHasher<AdminCredentials> _passwordHasher;

        public AuthController(JwtTokenService jwtTokenService,
            AdminCredentialsManager adminCredentialsManager,IPasswordHasher<AdminCredentials> passwordHasher)
        {
            _jwtTokenService = jwtTokenService;
            _adminCredentialsManager = adminCredentialsManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest request)
        {
            var credentials = _adminCredentialsManager.GetCredentials();
            if(request.Username != credentials.Username)
            {
                return Unauthorized(new 
                {
                    Message = "Invalid usename"
                });
            }

            var passwordHasher = new PasswordHasher<AdminCredentials>();

            var isPasswordValid = passwordHasher.VerifyHashedPassword(new AdminCredentials(),credentials.PasswordHash,request.Password);

            if(isPasswordValid != PasswordVerificationResult.Success)
            {
                return Unauthorized(new
                {
                    message = "Invalid Password"
                });
            }

            var token = _jwtTokenService.GenerateJwtToken(request.Username,"Admin");
            return Ok(token);
        }


        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var credentials = _adminCredentialsManager.GetCredentials();

            var passwordHasher = new PasswordHasher<AdminCredentials>();

            var isPasswordValid = passwordHasher.VerifyHashedPassword(new AdminCredentials(), credentials.PasswordHash, changePasswordRequest.CurrentPassword);
            if (isPasswordValid !=PasswordVerificationResult.Success)
            {
                return Unauthorized(new { message = "Current password is incorrect." });
            }

            var newPasswordHash = _passwordHasher.HashPassword(new AdminCredentials(),changePasswordRequest.NewPassword);

            _adminCredentialsManager.UpdateCredentials(newPasswordHash,changePasswordRequest.NewPassword);

            return Ok(new
            {
                message = "Password changed successfully"
            });
        }

    }
}
