using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> GetAllModelosAsync();
        Task<Modelo> GetModeloByIdAsync(int id);
        Task CreateModeloAsync(Modelo modelo);
        Task UpdateModeloAsync(Modelo modelo);
        Task DeleteModeloAsync(int id);
        Task<IEnumerable<Modelo>> GetModelosPorFabricanteAsync(int fabricanteId);
        Task<IEnumerable<Modelo>> GetModelosPorTipoAsync(string tipo);
    }
}
