using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ModelosController : ControllerBase
{
    private readonly IModeloService _modeloService;

    public ModelosController(IModeloService modeloService)
    {
        _modeloService = modeloService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetAll()
    {
        var modelos = await _modeloService.GetAllModelosAsync();
        return Ok(modelos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Modelo>> GetById(int id)
    {
        var modelo = await _modeloService.GetModeloByIdAsync(id);
        if (modelo == null) return NotFound();
        return Ok(modelo);
    }

    [HttpGet("por-fabricante/{fabricanteId}")]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetPorFabricante(int fabricanteId)
    {
        var modelos = await _modeloService.GetModelosPorFabricanteAsync(fabricanteId);
        return Ok(modelos);
    }

    [HttpGet("por-tipo/{tipo}")]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetPorTipo(string tipo)
    {
        var modelos = await _modeloService.GetModelosPorTipoAsync(tipo);
        return Ok(modelos);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Modelo modelo)
    {
        await _modeloService.CreateModeloAsync(modelo);
        return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Modelo modelo)
    {
        if (id != modelo.Id) return BadRequest();

        try
        {
            await _modeloService.UpdateModeloAsync(modelo);
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
            await _modeloService.DeleteModeloAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}