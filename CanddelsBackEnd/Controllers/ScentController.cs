using CanddelsBackEnd.Models;
using CanddelsBackEnd.Repositories.ScentsRepo;
using Microsoft.AspNetCore.Mvc;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScentController: ControllerBase
    {
        private readonly IScentRepository _scentRepository;

        public ScentController(IScentRepository scentRepository)
        {
            _scentRepository = scentRepository;
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

  
        [HttpPost]
        public async Task<ActionResult<Scent>> AddScent(Scent scent)
        {
            var addedScent = await _scentRepository.AddScentAsync(scent);
            return CreatedAtAction(nameof(GetScentById), new { id = addedScent.Id }, addedScent);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Scent>> UpdateScent(int id, Scent scent)
        {
            if (id != scent.Id)
            {
                return BadRequest();
            }

            var updatedScent = await _scentRepository.UpdateScentAsync(id, scent);
            if (updatedScent == null)
            {
                return NotFound();
            }

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

