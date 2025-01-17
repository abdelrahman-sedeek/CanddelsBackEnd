using CanddelsBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _filePath;
        public MessageController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _filePath = Path.Combine(_environment.WebRootPath, "data", "message.json");

        }


        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {

            if(!System.IO.File.Exists(_filePath))
            {
                return NotFound("file not exist");
            }

            var json = await System.IO.File.ReadAllTextAsync(_filePath);

            var message = JsonSerializer.Deserialize<MessageModel>(json);

            if (message == null)
            {
                return BadRequest(new { Error = "Failed to parse the message file." });
            }

            return Ok(message);


        }


        [HttpPost("update-message")]
        public async Task<IActionResult> UpdateMessage([FromBody]MessageModel newMessage)
        {
            if(newMessage == null && string.IsNullOrEmpty(newMessage?.Message))
            {
                return BadRequest("Invalid message content.");

            }

            var json = JsonSerializer.Serialize(newMessage,new JsonSerializerOptions { WriteIndented=true});
            var directory = Path.GetDirectoryName(_filePath);
            
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await System.IO.File.WriteAllTextAsync(_filePath, json);


            return Ok();
        }

    }
}
