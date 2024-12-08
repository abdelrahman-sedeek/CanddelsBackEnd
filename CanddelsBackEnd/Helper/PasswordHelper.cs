using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace CanddelsBackEnd.Helper
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes); 
            }
        }
        public static bool VerifyPassword(string enteredPassword,string storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(enteredPassword);
                var hashBytes = hmac.ComputeHash(passwordBytes);
                var newPasswordHash = Convert.ToBase64String(hashBytes);

                return storedHash == newPasswordHash; 
            }
        }
    }
}
