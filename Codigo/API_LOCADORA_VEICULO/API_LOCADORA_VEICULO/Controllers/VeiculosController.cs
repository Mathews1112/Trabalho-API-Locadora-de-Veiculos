using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;

    public VeiculosController(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    /// <summary>
    /// Retorna todos os veículos cadastrados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Veiculo>>> GetAll()
    {
        var veiculos = await _veiculoService.GetAllVeiculosAsync();
        return Ok(veiculos);
    }

    /// <summary>
    /// Retorna um veículo específico pelo ID.
    /// </summary>
    /// <param name="id">ID do veículo</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Veiculo>> GetById(int id)
    {
        var veiculo = await _veiculoService.GetVeiculoByIdAsync(id);
        if (veiculo == null) return NotFound();
        return Ok(veiculo);
    }

    /// <summary>
    /// Cria um novo veículo.
    /// </summary>
    /// <param name="veiculo">Objeto Veiculo a ser criado</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Veiculo veiculo)
    {
        try
        {
            await _veiculoService.CreateVeiculoAsync(veiculo);
            return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, veiculo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um veículo existente.
    /// </summary>
    /// <param name="id">ID do veículo</param>
    /// <param name="veiculo">Objeto Veiculo com os dados atualizados</param>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Veiculo veiculo)
    {
        if (id != veiculo.Id) return BadRequest();

        try
        {
            await _veiculoService.UpdateVeiculoAsync(veiculo);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Exclui um veículo pelo ID.
    /// </summary>
    /// <param name="id">ID do veículo</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _veiculoService.DeleteVeiculoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retorna os veículos disponíveis para aluguel.
    /// </summary>
    [HttpGet("disponiveis")]
    public async Task<ActionResult<IEnumerable<Veiculo>>> GetDisponiveis()
    {
        var veiculos = await _veiculoService.GetVeiculosDisponiveisParaAluguelAsync();
        return Ok(veiculos);
    }

    /// <summary>
    /// Retorna os veículos por modelo.
    /// </summary>
    /// <param name="modelo">Nome do modelo</param>
    [HttpGet("por-modelo/{modelo}")]
    public async Task<ActionResult<IEnumerable<Veiculo>>> GetPorModelo(string modelo)
    {
        var veiculos = await _veiculoService.GetVeiculosByModeloAsync(modelo);
        return Ok(veiculos);
    }

    /// <summary>
    /// Retorna os veículos por status.
    /// </summary>
    /// <param name="status">Status do veículo (ex: Disponível, Alugado, Manutenção)</param>
    [HttpGet("por-status/{status}")]
    public async Task<ActionResult<IEnumerable<Veiculo>>> GetPorStatus(string status)
    {
        var veiculos = await _veiculoService.GetVeiculosByStatusAsync(status);
        return Ok(veiculos);
    }
}
