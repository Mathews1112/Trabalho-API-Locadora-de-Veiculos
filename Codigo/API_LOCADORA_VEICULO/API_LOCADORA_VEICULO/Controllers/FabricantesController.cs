using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FabricantesController : ControllerBase
{
    private readonly IFabricanteService _fabricanteService;

    public FabricantesController(IFabricanteService fabricanteService)
    {
        _fabricanteService = fabricanteService;
    }

    /// <summary>
    /// Retorna todos os fabricantes cadastrados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fabricante>>> GetAll()
    {
        var fabricantes = await _fabricanteService.GetAllFabricantesAsync();
        return Ok(fabricantes);
    }

    /// <summary>
    /// Retorna um fabricante pelo ID.
    /// </summary>
    /// <param name="id">ID do fabricante</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Fabricante>> GetById(int id)
    {
        var fabricante = await _fabricanteService.GetFabricanteByIdAsync(id);
        if (fabricante == null) return NotFound();
        return Ok(fabricante);
    }

    /// <summary>
    /// Cria um novo fabricante.
    /// </summary>
    /// <param name="fabricante">Objeto Fabricante</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Fabricante fabricante)
    {
        await _fabricanteService.CreateFabricanteAsync(fabricante);
        return CreatedAtAction(nameof(GetById), new { id = fabricante.Id }, fabricante);
    }

    /// <summary>
    /// Atualiza um fabricante existente.
    /// </summary>
    /// <param name="id">ID do fabricante</param>
    /// <param name="fabricante">Objeto Fabricante atualizado</param>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Fabricante fabricante)
    {
        if (id != fabricante.Id) return BadRequest();

        try
        {
            await _fabricanteService.UpdateFabricanteAsync(fabricante);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Exclui um fabricante pelo ID.
    /// </summary>
    /// <param name="id">ID do fabricante</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _fabricanteService.DeleteFabricanteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
