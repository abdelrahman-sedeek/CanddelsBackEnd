using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.ScentsRepo;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScentController: ControllerBase
    {
        private readonly IScentRepository _scentRepository;
        private readonly FileUploadService _fileUploadService;

        public ScentController(IScentRepository scentRepository, FileUploadService fileUploadService)
        {
            _scentRepository = scentRepository;
            _fileUploadService = fileUploadService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Scent>>> GetAllScents()
        {
            var scents = await _scentRepository.GetAllScentsAsync();
            return Ok(scents);
        }

   
        [HttpGet("{id}")]
        public async Task<ActionResult<Scent>> GetScentById(int id)
        {
            var scent = await _scentRepository.GetScentByIdAsync(id);
            if (scent == null)
            {
                return NotFound();
            }
            return Ok(scent);
        }


        [HttpPost("add-scent")]
        public async Task<ActionResult<Scent>> AddScent([FromForm] Scent scent, IFormFile? image)
        {
            if (image != null)
            {
                scent.ImageUrl = await _fileUploadService.UploadImage(image, "scents");
            }

            var addedScent = await _scentRepository.AddScentAsync(scent);
            return CreatedAtAction(nameof(GetScentById), new { id = addedScent.Id }, addedScent);
        }


        [HttpPut("update-scent/{id}")]
        public async Task<ActionResult<Scent>> UpdateScent(int id, [FromForm] ScentDto scent, IFormFile? image)
        {
            if (id != scent.Id)
            {
                return BadRequest();
            }

            var existingScent = await _scentRepository.GetScentByIdAsync(id);
            if (existingScent == null)
            {
                return NotFound();
            }

            // Update properties
            existingScent.Name = scent.Name;
            existingScent.Description = scent.Description;

            // Handle image upload or removal
            if (scent.ImageUrl == "null")
            {
                existingScent.ImageUrl = null; // Remove the image
            }
            else if (image != null)
            {
                existingScent.ImageUrl = await _fileUploadService.UploadImage(image, "scents");
            }

            var updatedScent = await _scentRepository.UpdateScentAsync(id, existingScent);
            return Ok(updatedScent);
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult<bool>> DeleteScent(int id)
        {
            var result = await _scentRepository.DeleteScentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

