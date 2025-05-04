using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlugueisController : ControllerBase
{
    private readonly IAluguelService _aluguelService;

    public AlugueisController(IAluguelService aluguelService)
    {
        _aluguelService = aluguelService;
    }

    /// <summary>
    /// Retorna todos os aluguéis.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetAll()
    {
        var alugueis = await _aluguelService.GetAllAlugueisAsync();
        return Ok(alugueis);
    }

    /// <summary>
    /// Retorna um aluguel pelo ID.
    /// </summary>
    /// <param name="id">ID do aluguel</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Aluguel>> GetById(int id)
    {
        var aluguel = await _aluguelService.GetAluguelByIdAsync(id);
        if (aluguel == null) return NotFound();
        return Ok(aluguel);
    }

    /// <summary>
    /// Cria um novo aluguel.
    /// </summary>
    /// <param name="aluguel">Objeto Aluguel</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Aluguel aluguel)
    {
        try
        {
            await _aluguelService.CreateAluguelAsync(aluguel);
            return CreatedAtAction(nameof(GetById), new { id = aluguel.Id }, aluguel);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um aluguel existente.
    /// </summary>
    /// <param name="id">ID do aluguel</param>
    /// <param name="aluguel">Objeto Aluguel atualizado</param>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Aluguel aluguel)
    {
        if (id != aluguel.Id) return BadRequest();

        try
        {
            await _aluguelService.UpdateAluguelAsync(aluguel);
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
    /// Exclui um aluguel pelo ID.
    /// </summary>
    /// <param name="id">ID do aluguel</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _aluguelService.DeleteAluguelAsync(id);
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
    /// Finaliza um aluguel, informando a quilometragem final e observações.
    /// </summary>
    /// <param name="id">ID do aluguel</param>
    /// <param name="dto">Dados de finalização</param>
    [HttpPost("{id}/finalizar")]
    public async Task<ActionResult> FinalizarAluguel(int id, [FromBody] FinalizarAluguelDto dto)
    {
        try
        {
            await _aluguelService.FinalizarAluguelAsync(id, dto.QuilometragemFinal, dto.Observacoes);
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
    /// Retorna todos os aluguéis ativos (não finalizados).
    /// </summary>
    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetAtivos()
    {
        var alugueis = await _aluguelService.GetAlugueisAtivosComVeiculoEClienteAsync();
        return Ok(alugueis);
    }

    /// <summary>
    /// Retorna aluguéis de um cliente específico.
    /// </summary>
    /// <param name="clienteId">ID do cliente</param>
    [HttpGet("por-cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorCliente(int clienteId)
    {
        var alugueis = await _aluguelService.GetAlugueisPorClienteAsync(clienteId);
        return Ok(alugueis);
    }

    /// <summary>
    /// Retorna aluguéis em um determinado intervalo de datas.
    /// </summary>
    /// <param name="inicio">Data de início</param>
    /// <param name="fim">Data de fim</param>
    [HttpGet("por-periodo")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var alugueis = await _aluguelService.GetAlugueisPorPeriodoAsync(inicio, fim);
        return Ok(alugueis);
    }

    /// <summary>
    /// Retorna aluguéis que incorreram em multa.
    /// </summary>
    [HttpGet("com-multa")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetComMulta()
    {
        var alugueis = await _aluguelService.GetAlugueisComMultaAsync();
        return Ok(alugueis);
    }

    /// <summary>
    /// Retorna aluguéis de um veículo específico.
    /// </summary>
    /// <param name="veiculoId">ID do veículo</param>
    [HttpGet("por-veiculo/{veiculoId}")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorVeiculo(int veiculoId)
    {
        var alugueis = await _aluguelService.GetAlugueisPorVeiculoAsync(veiculoId);
        return Ok(alugueis);
    }
}

/// <summary>
/// DTO para finalizar um aluguel.
/// </summary>
public class FinalizarAluguelDto
{
    /// <summary>
    /// Quilometragem final do veículo.
    /// </summary>
    public int QuilometragemFinal { get; set; }

    /// <summary>
    /// Observações sobre o retorno do veículo.
    /// </summary>
    public string Observacoes { get; set; }
}
