using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoresController : ControllerBase
{
    private readonly ICorService _corService;

    public CoresController(ICorService corService)
    {
        _corService = corService;
    }

    /// <summary>
    /// Retorna todas as cores cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cor>>> GetAll()
    {
        var cores = await _corService.GetAllCoresAsync();
        return Ok(cores);
    }

    /// <summary>
    /// Retorna uma cor pelo ID.
    /// </summary>
    /// <param name="id">ID da cor</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cor>> GetById(int id)
    {
        var cor = await _corService.GetCorByIdAsync(id);
        if (cor == null) return NotFound();
        return Ok(cor);
    }

    /// <summary>
    /// Cria uma nova cor.
    /// </summary>
    /// <param name="cor">Objeto Cor</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Cor cor)
    {
        await _corService.CreateCorAsync(cor);
        return CreatedAtAction(nameof(GetById), new { id = cor.Id }, cor);
    }

    /// <summary>
    /// Atualiza uma cor existente.
    /// </summary>
    /// <param name="id">ID da cor</param>
    /// <param name="cor">Objeto Cor atualizado</param>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Cor cor)
    {
        if (id != cor.Id) return BadRequest();

        try
        {
            await _corService.UpdateCorAsync(cor);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Exclui uma cor pelo ID.
    /// </summary>
    /// <param name="id">ID da cor</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _corService.DeleteCorAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
