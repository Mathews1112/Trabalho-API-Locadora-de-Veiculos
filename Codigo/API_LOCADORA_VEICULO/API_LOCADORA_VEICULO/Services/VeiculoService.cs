using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly AppDbContext _context;

        public VeiculoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateVeiculoAsync(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new ArgumentNullException(nameof(veiculo));

            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVeiculoAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                throw new KeyNotFoundException("Veículo não encontrado");

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Veiculo>> GetAllVeiculosAsync()
        {
            return await _context.Veiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Fabricante)
                .Include(v => v.Cor)
                .ToListAsync();
        }

        public async Task<Veiculo> GetVeiculoByIdAsync(int id)
        {
            return await _context.Veiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Fabricante)
                .Include(v => v.Cor)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculosByModeloAsync(string modelo)
        {
            return await _context.Veiculos
                .Include(v => v.Modelo)
                .Where(v => v.Modelo.Descricao.Contains(modelo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculosByStatusAsync(string status)
        {
            return await _context.Veiculos
                .Where(v => v.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculosDisponiveisParaAluguelAsync()
        {
            return await _context.Veiculos
                .Where(v => v.Status == "Disponível")
                .ToListAsync();
        }

        public async Task UpdateVeiculoAsync(Veiculo veiculo)
        {
            _context.Entry(veiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
