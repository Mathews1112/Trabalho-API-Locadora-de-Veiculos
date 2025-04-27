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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetAll()
    {
        var alugueis = await _aluguelService.GetAllAlugueisAsync();
        return Ok(alugueis);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Aluguel>> GetById(int id)
    {
        var aluguel = await _aluguelService.GetAluguelByIdAsync(id);
        if (aluguel == null) return NotFound();
        return Ok(aluguel);
    }

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

    // Endpoints para os filtros
    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetAtivos()
    {
        var alugueis = await _aluguelService.GetAlugueisAtivosComVeiculoEClienteAsync();
        return Ok(alugueis);
    }

    [HttpGet("por-cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorCliente(int clienteId)
    {
        var alugueis = await _aluguelService.GetAlugueisPorClienteAsync(clienteId);
        return Ok(alugueis);
    }

    [HttpGet("por-periodo")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var alugueis = await _aluguelService.GetAlugueisPorPeriodoAsync(inicio, fim);
        return Ok(alugueis);
    }

    [HttpGet("com-multa")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetComMulta()
    {
        var alugueis = await _aluguelService.GetAlugueisComMultaAsync();
        return Ok(alugueis);
    }

    [HttpGet("por-veiculo/{veiculoId}")]
    public async Task<ActionResult<IEnumerable<Aluguel>>> GetPorVeiculo(int veiculoId)
    {
        var alugueis = await _aluguelService.GetAlugueisPorVeiculoAsync(veiculoId);
        return Ok(alugueis);
    }
}

public class FinalizarAluguelDto
{
    public int QuilometragemFinal { get; set; }
    public string Observacoes { get; set; }
}