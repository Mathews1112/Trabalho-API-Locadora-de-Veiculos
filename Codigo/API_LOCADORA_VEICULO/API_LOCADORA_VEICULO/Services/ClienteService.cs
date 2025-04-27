using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateClienteAsync(Cliente cliente)
        {
            if (await _context.Clientes.AnyAsync(c => c.CPF == cliente.CPF))
                throw new InvalidOperationException("Já existe um cliente com este CPF");

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente não encontrado");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Cartoes)
                .ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Enderecos)
                .Include(c => c.Cartoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> GetClientesAtivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientesComAluguelAtivoAsync()
        {
            return await _context.Clientes
                .Where(c => c.Alugueis.Any(a => a.Status == "Ativo"))
                .ToListAsync();
        }

        public async Task<Cliente> GetClienteByCpfAsync(string cpf)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
