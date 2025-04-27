using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> GetAllVeiculosAsync();
        Task<Veiculo> GetVeiculoByIdAsync(int id);
        Task CreateVeiculoAsync(Veiculo veiculo);
        Task UpdateVeiculoAsync(Veiculo veiculo);
        Task DeleteVeiculoAsync(int id);
        Task<IEnumerable<Veiculo>> GetVeiculosByModeloAsync(string modelo);
        Task<IEnumerable<Veiculo>> GetVeiculosByStatusAsync(string status);
        Task<IEnumerable<Veiculo>> GetVeiculosDisponiveisParaAluguelAsync();
    }
}
