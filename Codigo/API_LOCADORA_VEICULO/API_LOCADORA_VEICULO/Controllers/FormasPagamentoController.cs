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

    /// <summary>
    /// Retorna todas as formas de pagamento cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetAll()
    {
        var formas = await _formaPagamentoService.GetAllFormasPagamentoAsync();
        return Ok(formas);
    }

    /// <summary>
    /// Retorna uma forma de pagamento pelo ID.
    /// </summary>
    /// <param name="id">ID da forma de pagamento</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamento>> GetById(int id)
    {
        var forma = await _formaPagamentoService.GetFormaPagamentoByIdAsync(id);
        if (forma == null) return NotFound();
        return Ok(forma);
    }

    /// <summary>
    /// Retorna todas as formas de pagamento ativas.
    /// </summary>
    [HttpGet("ativas")]
    public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetAtivas()
    {
        var formas = await _formaPagamentoService.GetFormasPagamentoAtivasAsync();
        return Ok(formas);
    }

    /// <summary>
    /// Cria uma nova forma de pagamento.
    /// </summary>
    /// <param name="formaPagamento">Objeto FormaPagamento</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FormaPagamento formaPagamento)
    {
        await _formaPagamentoService.CreateFormaPagamentoAsync(formaPagamento);
        return CreatedAtAction(nameof(GetById), new { id = formaPagamento.Id }, formaPagamento);
    }

    /// <summary>
    /// Atualiza uma forma de pagamento existente.
    /// </summary>
    /// <param name="id">ID da forma de pagamento</param>
    /// <param name="formaPagamento">Objeto FormaPagamento atualizado</param>
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

    /// <summary>
    /// Exclui uma forma de pagamento pelo ID.
    /// </summary>
    /// <param name="id">ID da forma de pagamento</param>
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
