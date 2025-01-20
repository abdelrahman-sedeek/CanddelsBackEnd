using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;


namespace CanddelsBackEnd.Repositories.ScentsRepo
{
    public class ScentRepository:IScentRepository
    {
        private readonly CandelContext _context;

        public ScentRepository(CandelContext context)
        {
            _context = context;
        }

        public async Task<List<Scent>> GetAllScentsAsync()
        {
            return await _context.Scents.ToListAsync();
        }

        public async Task<Scent> GetScentByIdAsync(int id)
        {
            return await _context.Scents.FindAsync(id);
        }

        public async Task<Scent> AddScentAsync(Scent scent)
        {
            _context.Scents.Add(scent);
            await _context.SaveChangesAsync();
            return scent;
        }

        public async Task<Scent> UpdateScentAsync(int id, Scent scent)
        {
            var existingScent = await _context.Scents.FindAsync(id);
            if (existingScent == null)
            {
                return null;
            }

            existingScent.Name = scent.Name;
            existingScent.Description = scent.Description;

            _context.Scents.Update(existingScent);
            await _context.SaveChangesAsync();
            return existingScent;
        }

        public async Task<bool> DeleteScentAsync(int id)
        {
            var scent = await _context.Scents.FindAsync(id);
            if (scent == null)
            {
                return false;
            }

            _context.Scents.Remove(scent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

