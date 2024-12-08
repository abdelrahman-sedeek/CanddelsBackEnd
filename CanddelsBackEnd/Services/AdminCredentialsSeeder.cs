using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CanddelsBackEnd.Services
{
    public class AdminCredentialsSeeder
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<AdminCredentials> _passwordHasher;
        private readonly string _configFilePath = "appsettings.json";

        public AdminCredentialsSeeder(IConfiguration configuration,IPasswordHasher<AdminCredentials> passwordHasher)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }
        public  void SeedAdminPassword(string password)
        {
            var adminCredentials = _configuration.GetSection("AdminCredentials").Get<AdminCredentials>();

            if (!string.IsNullOrEmpty(adminCredentials.PasswordHash)) return;

            var passwordHash = _passwordHasher.HashPassword(new AdminCredentials(),password);

            var configJson = File.ReadAllText(_configFilePath);
            
            dynamic config = JsonConvert.DeserializeObject(configJson);

            config["AdminCredentials"]["PasswordHash"] = passwordHash;
            config["AdminCredentials"]["Password"] = password;

            File.WriteAllText(_configFilePath, JsonConvert.SerializeObject(config,Formatting.Indented));
    
        }
    }
}
