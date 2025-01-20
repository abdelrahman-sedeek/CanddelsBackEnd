using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Repositories.ScentsRepo
{
    public interface IScentRepository
    {
        Task<List<Scent>> GetAllScentsAsync();
        Task<Scent> GetScentByIdAsync(int id);
        Task<Scent> AddScentAsync(Scent scent);
        Task<Scent> UpdateScentAsync(int id, Scent scent);
        Task<bool> DeleteScentAsync(int id);
    }
}
