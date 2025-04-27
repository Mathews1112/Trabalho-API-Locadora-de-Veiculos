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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fabricante>>> GetAll()
    {
        var fabricantes = await _fabricanteService.GetAllFabricantesAsync();
        return Ok(fabricantes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Fabricante>> GetById(int id)
    {
        var fabricante = await _fabricanteService.GetFabricanteByIdAsync(id);
        if (fabricante == null) return NotFound();
        return Ok(fabricante);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Fabricante fabricante)
    {
        await _fabricanteService.CreateFabricanteAsync(fabricante);
        return CreatedAtAction(nameof(GetById), new { id = fabricante.Id }, fabricante);
    }

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