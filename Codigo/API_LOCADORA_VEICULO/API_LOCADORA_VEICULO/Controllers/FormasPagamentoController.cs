using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FormasPagamentoController : ControllerBase
{
    private readonly IFormaPagamentoService _formaPagamentoService;

    public FormasPagamentoController(IFormaPagamentoService formaPagamentoService)
    {
        _formaPagamentoService = formaPagamentoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetAll()
    {
        var formas = await _formaPagamentoService.GetAllFormasPagamentoAsync();
        return Ok(formas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamento>> GetById(int id)
    {
        var forma = await _formaPagamentoService.GetFormaPagamentoByIdAsync(id);
        if (forma == null) return NotFound();
        return Ok(forma);
    }

    [HttpGet("ativas")]
    public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetAtivas()
    {
        var formas = await _formaPagamentoService.GetFormasPagamentoAtivasAsync();
        return Ok(formas);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FormaPagamento formaPagamento)
    {
        await _formaPagamentoService.CreateFormaPagamentoAsync(formaPagamento);
        return CreatedAtAction(nameof(GetById), new { id = formaPagamento.Id }, formaPagamento);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] FormaPagamento formaPagamento)
    {
        if (id != formaPagamento.Id) return BadRequest();

        try
        {
            await _formaPagamentoService.UpdateFormaPagamentoAsync(formaPagamento);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _formaPagamentoService.DeleteFormaPagamentoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}