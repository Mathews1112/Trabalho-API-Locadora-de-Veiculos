using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IFabricanteService
    {
        Task<IEnumerable<Fabricante>> GetAllFabricantesAsync();
        Task<Fabricante> GetFabricanteByIdAsync(int id);
        Task CreateFabricanteAsync(Fabricante fabricante);
        Task UpdateFabricanteAsync(Fabricante fabricante);
        Task DeleteFabricanteAsync(int id);
    }
}
