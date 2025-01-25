using Microsoft.AspNetCore.Mvc;
using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using CanddelsBackEnd.Services;
using CanddelsBackEnd.Contexts;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly CandelContext _context;
        private readonly FileUploadService _fileUploadService;

        public MessageController(CandelContext context, FileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {
            var message = await _context.Messages.FirstOrDefaultAsync();
            if (message == null)
            {
                return NotFound("No message found.");
            }
            return Ok(message);
        }

        [HttpPost("update-message")]
        public async Task<IActionResult> UpdateMessage([FromForm] string? text, IFormFile? image)
        {
            var existingMessage = await _context.Messages.FirstOrDefaultAsync();
            if (existingMessage == null)
            {
                existingMessage = new Message();
                _context.Messages.Add(existingMessage);
            }

            // Update the text (even if it's empty)
            existingMessage.Text = text is null ? "":text;

            // Handle image update or removal
            if (image != null)
            {
                try
                {
                    var imageUrl = await _fileUploadService.UploadImage(image, "messages");
                    existingMessage.ImageUrl = imageUrl;
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                // If no image is provided, clear the existing image URL
                existingMessage.ImageUrl = null;
            }

            await _context.SaveChangesAsync();
            return Ok(existingMessage);
        }
    }
}