using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAtivosAsync();
        Task<IEnumerable<Cliente>> GetClientesComAluguelAtivoAsync();
        Task<Cliente> GetClienteByCpfAsync(string cpf);
    }

}
