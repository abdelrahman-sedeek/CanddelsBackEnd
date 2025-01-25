using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Models;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanddelsBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly CandelContext _context;
        private readonly FileUploadService _fileUploadService;

        public StoreController(CandelContext context, FileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        // POST: api/Store
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore([FromForm] StoreDto storeDTO)
        {
            if (storeDTO.ImageFile == null)
            {
                return BadRequest("Image file is required.");
            }

            // Upload the image and get the URL
            var imageUrl = await _fileUploadService.UploadImage(storeDTO.ImageFile, "stores");

            // Map StoreDTO to Store entity
            var store = new Store
            {
                Name = storeDTO.Name,
                Address = storeDTO.Address,
                Imageurl = imageUrl
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, [FromForm] StoreDto storeDTO)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            // Update the store properties
            store.Name = storeDTO.Name;
            store.Address = storeDTO.Address;

            // If a new image is provided, upload it and update the Imageurl
            if (storeDTO.ImageFile != null)
            {
                store.Imageurl = await _fileUploadService.UploadImage(storeDTO.ImageFile, "stores");
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}