using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CanddelsBackEnd.Dtos;
using Microsoft.Extensions.Caching.Memory;

public partial class EmailService
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<EmailSettings> _emailSettings;

    //private static ConcurrentDictionary<string, TokenInfo> TokenStore = new(); 
    public EmailService(
        IOptions<EmailSettings> emailSettings,
        IOptions<JwtOptions> jwtOptions,
        IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _emailSettings = emailSettings;
        _jwtOptions = jwtOptions;
    }

    public async Task SendVerificationEmail(string toEmail)
    {
        var verificationToken = GenerateVerificationToken(toEmail);

        var tokenInfo = new TokenInfo
        {
            Email = toEmail,
            Expiration = DateTime.Now.AddMinutes(30),
            Used = false
        };

        _memoryCache.Set(verificationToken,tokenInfo,new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
        });

        Console.WriteLine(tokenInfo);

        string subject = "Please Verify Your Email";
        string body = $"Click this link to verify your email: http://localhost:4200/order?token={verificationToken}";

        if(!_memoryCache.TryGetValue(verificationToken,out var tokeninfo))
        {
            Console.WriteLine(tokeninfo);
        }
        Console.WriteLine(tokeninfo);

        var mailMessage = new MimeMessage();
        mailMessage.From.Add(MailboxAddress.Parse(_emailSettings.Value.SmtpUsername));
        mailMessage.To.Add(MailboxAddress.Parse(toEmail));
        mailMessage.Subject = subject;
        mailMessage.Body = new TextPart("plain") { Text = body };

        try
        {
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Value.SmtpUsername, _emailSettings.Value.SmtpPassword);
            await smtp.SendAsync(mailMessage);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    public bool VerifyToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Token is null or empty.");
            return false;
        }

        if (!_memoryCache.TryGetValue(token, out TokenInfo tokenInfo))
        {
            Console.WriteLine("Token not found in store.");
            return false;
        }

        if (tokenInfo.Used || tokenInfo.Expiration < DateTime.Now)
        {
            Console.WriteLine("Token is used or expired.");
            return false;
        }  

        tokenInfo.Used = true; 
        return true;
    }


    private string GenerateVerificationToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim("tokenId", Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
