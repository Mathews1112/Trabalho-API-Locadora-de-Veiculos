using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IAluguelService
    {
        Task<IEnumerable<Aluguel>> GetAllAlugueisAsync();
        Task<Aluguel> GetAluguelByIdAsync(int id);
        Task CreateAluguelAsync(Aluguel aluguel);
        Task UpdateAluguelAsync(Aluguel aluguel);
        Task DeleteAluguelAsync(int id);
        Task FinalizarAluguelAsync(int id, int quilometragemFinal, string observacoes);

        // Filtros com joins
        Task<IEnumerable<Aluguel>> GetAlugueisAtivosComVeiculoEClienteAsync();
        Task<IEnumerable<Aluguel>> GetAlugueisPorClienteAsync(int clienteId);
        Task<IEnumerable<Aluguel>> GetAlugueisPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task<IEnumerable<Aluguel>> GetAlugueisComMultaAsync();
        Task<IEnumerable<Aluguel>> GetAlugueisPorVeiculoAsync(int veiculoId);
    }
}
