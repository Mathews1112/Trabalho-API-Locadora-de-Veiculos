using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        private readonly AppDbContext _context;

        public FormaPagamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            await _context.FormasPagamento.AddAsync(formaPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFormaPagamentoAsync(int id)
        {
            var formaPagamento = await _context.FormasPagamento.FindAsync(id);
            if (formaPagamento == null)
                throw new KeyNotFoundException("Forma de pagamento não encontrada");

            _context.FormasPagamento.Remove(formaPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FormaPagamento>> GetAllFormasPagamentoAsync()
        {
            return await _context.FormasPagamento.ToListAsync();
        }

        public async Task<IEnumerable<FormaPagamento>> GetFormasPagamentoAtivasAsync()
        {
            return await _context.FormasPagamento
                .Where(fp => fp.Ativo)
                .ToListAsync();
        }

        public async Task<FormaPagamento> GetFormaPagamentoByIdAsync(int id)
        {
            return await _context.FormasPagamento.FindAsync(id);
        }

        public async Task UpdateFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            _context.Entry(formaPagamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
