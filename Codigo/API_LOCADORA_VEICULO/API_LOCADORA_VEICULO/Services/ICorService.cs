using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface ICorService
    {
        Task<IEnumerable<Cor>> GetAllCoresAsync();
        Task<Cor> GetCorByIdAsync(int id);
        Task CreateCorAsync(Cor cor);
        Task UpdateCorAsync(Cor cor);
        Task DeleteCorAsync(int id);
    }
}
