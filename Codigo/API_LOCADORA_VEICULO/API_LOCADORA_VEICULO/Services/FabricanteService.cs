using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class FabricanteService : IFabricanteService
    {
        private readonly AppDbContext _context;

        public FabricanteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateFabricanteAsync(Fabricante fabricante)
        {
            await _context.Fabricantes.AddAsync(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFabricanteAsync(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
                throw new KeyNotFoundException("Fabricante não encontrado");

            _context.Fabricantes.Remove(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fabricante>> GetAllFabricantesAsync()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        public async Task<Fabricante> GetFabricanteByIdAsync(int id)
        {
            return await _context.Fabricantes.FindAsync(id);
        }

        public async Task UpdateFabricanteAsync(Fabricante fabricante)
        {
            _context.Entry(fabricante).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
