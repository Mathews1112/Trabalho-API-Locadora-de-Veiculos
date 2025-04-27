using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class ModeloService : IModeloService
    {
        private readonly AppDbContext _context;

        public ModeloService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateModeloAsync(Modelo modelo)
        {
            await _context.Modelos.AddAsync(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteModeloAsync(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                throw new KeyNotFoundException("Modelo não encontrado");

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Modelo>> GetAllModelosAsync()
        {
            return await _context.Modelos
                .Include(m => m.Fabricante)
                .ToListAsync();
        }

        public async Task<IEnumerable<Modelo>> GetModelosPorFabricanteAsync(int fabricanteId)
        {
            return await _context.Modelos
                .Where(m => m.Id_Fabricante == fabricanteId)
                .Include(m => m.Fabricante)
                .ToListAsync();
        }

        public async Task<IEnumerable<Modelo>> GetModelosPorTipoAsync(string tipo)
        {
            return await _context.Modelos
                .Where(m => m.Tipo == tipo)
                .Include(m => m.Fabricante)
                .ToListAsync();
        }

        public async Task<Modelo> GetModeloByIdAsync(int id)
        {
            return await _context.Modelos
                .Include(m => m.Fabricante)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateModeloAsync(Modelo modelo)
        {
            _context.Entry(modelo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
