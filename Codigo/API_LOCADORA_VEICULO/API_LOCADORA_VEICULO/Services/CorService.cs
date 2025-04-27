using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class CorService : ICorService
    {
        private readonly AppDbContext _context;

        public CorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCorAsync(Cor cor)
        {
            await _context.Cores.AddAsync(cor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCorAsync(int id)
        {
            var cor = await _context.Cores.FindAsync(id);
            if (cor == null)
                throw new KeyNotFoundException("Cor não encontrada");

            _context.Cores.Remove(cor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cor>> GetAllCoresAsync()
        {
            return await _context.Cores.ToListAsync();
        }

        public async Task<Cor> GetCorByIdAsync(int id)
        {
            return await _context.Cores.FindAsync(id);
        }

        public async Task UpdateCorAsync(Cor cor)
        {
            _context.Entry(cor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
