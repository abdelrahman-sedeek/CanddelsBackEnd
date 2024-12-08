using CanddelsBackEnd.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CanddelsBackEnd.Services
{
    public class AdminCredentialsManager
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<AdminCredentials> _optionsMonitor;
        private readonly string _configFilePath = "appsettings.json";

        public AdminCredentialsManager(IOptionsMonitor<AdminCredentials> optionsMonitor,IConfiguration configuration)
        {
            _configuration = configuration;
            _optionsMonitor = optionsMonitor;
        }


        public AdminCredentials GetCredentials()
        {
            return _optionsMonitor.CurrentValue;
        }

        public void UpdateCredentials(string newPasswordHash,string newPassword)
        {
            var configJson = File.ReadAllText(_configFilePath);

            dynamic config = JsonConvert.DeserializeObject(configJson);

            config["AdminCredentials"]["PasswordHash"] = newPasswordHash;
            config["AdminCredentials"]["Password"] = newPassword;

            File.WriteAllText(_configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}
