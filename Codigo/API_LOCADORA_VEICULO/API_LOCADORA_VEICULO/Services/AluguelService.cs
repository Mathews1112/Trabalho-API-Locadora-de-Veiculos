using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Services
{
    public class AluguelService : IAluguelService
    {
        private readonly AppDbContext _context;
        private readonly IVeiculoService _veiculoService;

        public AluguelService(AppDbContext context, IVeiculoService veiculoService)
        {
            _context = context;
            _veiculoService = veiculoService;
        }

        public async Task CreateAluguelAsync(Aluguel aluguel)
        {
            // Validações
            if (aluguel.DataInicio < DateTime.Today)
                throw new InvalidOperationException("Data de início não pode ser no passado");

            var veiculo = await _veiculoService.GetVeiculoByIdAsync(aluguel.Id_Veiculo);
            if (veiculo.Status != "Disponível")
                throw new InvalidOperationException("Veículo não está disponível para aluguel");

            // Configurações iniciais
            aluguel.Status = "Ativo";
            aluguel.QuilometragemInicial = veiculo.Quilometragem;
            aluguel.ValorTotal = CalculateValorTotal(aluguel);

            // Atualiza status do veículo
            veiculo.Status = "Alugado";
            await _veiculoService.UpdateVeiculoAsync(veiculo);

            await _context.Alugueis.AddAsync(aluguel);
            await _context.SaveChangesAsync();
        }

        private decimal CalculateValorTotal(Aluguel aluguel)
        {
            int dias = (int)(aluguel.DataDevolucaoPrevista - aluguel.DataInicio).TotalDays;
            return dias * aluguel.ValorDiaria;
        }

        public async Task DeleteAluguelAsync(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
                throw new KeyNotFoundException("Aluguel não encontrado");

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();
        }

        public async Task FinalizarAluguelAsync(int id, int quilometragemFinal, string observacoes)
        {
            var aluguel = await _context.Alugueis
                .Include(a => a.Veiculo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluguel == null)
                throw new KeyNotFoundException("Aluguel não encontrado");

            // Atualiza dados do aluguel
            aluguel.DataDevolucaoReal = DateTime.Now;
            aluguel.QuilometragemFinal = quilometragemFinal;
            aluguel.Observacoes = observacoes;
            aluguel.Status = "Finalizado";

            // Calcula multa se houver atraso
            if (aluguel.DataDevolucaoReal > aluguel.DataDevolucaoPrevista)
            {
                int diasAtraso = (int)(aluguel.DataDevolucaoReal.Value - aluguel.DataDevolucaoPrevista).TotalDays;
                aluguel.ValorMulta = diasAtraso * (aluguel.ValorDiaria * 0.2m); // 20% da diária por dia de atraso
            }

            // Atualiza quilometragem do veículo
            var veiculo = await _veiculoService.GetVeiculoByIdAsync(aluguel.Id_Veiculo);
            veiculo.Quilometragem = quilometragemFinal;
            veiculo.Status = "Disponível";
            await _veiculoService.UpdateVeiculoAsync(veiculo);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAllAlugueisAsync()
        {
            return await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                    .ThenInclude(v => v.Modelo)
                .Include(a => a.FormaPagamento)
                .ToListAsync();
        }

        public async Task<Aluguel> GetAluguelByIdAsync(int id)
        {
            return await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                    .ThenInclude(v => v.Modelo)
                .Include(a => a.FormaPagamento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAluguelAsync(Aluguel aluguel)
        {
            _context.Entry(aluguel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Implementações dos filtros com joins
        public async Task<IEnumerable<Aluguel>> GetAlugueisAtivosComVeiculoEClienteAsync()
        {
            return await _context.Alugueis
                .Where(a => a.Status == "Ativo")
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorClienteAsync(int clienteId)
        {
            return await _context.Alugueis
                .Where(a => a.Id_Cliente == clienteId)
                .Include(a => a.Veiculo)
                .OrderByDescending(a => a.DataInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Alugueis
                .Where(a => a.DataInicio >= inicio && a.DataInicio <= fim)
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisComMultaAsync()
        {
            return await _context.Alugueis
                .Where(a => a.ValorMulta > 0)
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluguel>> GetAlugueisPorVeiculoAsync(int veiculoId)
        {
            return await _context.Alugueis
                .Where(a => a.Id_Veiculo == veiculoId)
                .Include(a => a.Cliente)
                .OrderByDescending(a => a.DataInicio)
                .ToListAsync();
        }
    }
}
