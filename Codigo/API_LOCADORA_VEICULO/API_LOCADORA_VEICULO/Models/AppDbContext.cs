using LocadoraVeiculos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Cor> Cores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
    }
}