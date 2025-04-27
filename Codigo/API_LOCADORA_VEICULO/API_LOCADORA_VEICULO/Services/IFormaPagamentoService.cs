using LocadoraVeiculos.Models.Entities;

namespace LocadoraVeiculos.Services
{
    public interface IFormaPagamentoService
    {
        Task<IEnumerable<FormaPagamento>> GetAllFormasPagamentoAsync();
        Task<FormaPagamento> GetFormaPagamentoByIdAsync(int id);
        Task CreateFormaPagamentoAsync(FormaPagamento formaPagamento);
        Task UpdateFormaPagamentoAsync(FormaPagamento formaPagamento);
        Task DeleteFormaPagamentoAsync(int id);
        Task<IEnumerable<FormaPagamento>> GetFormasPagamentoAtivasAsync();
    }
}
