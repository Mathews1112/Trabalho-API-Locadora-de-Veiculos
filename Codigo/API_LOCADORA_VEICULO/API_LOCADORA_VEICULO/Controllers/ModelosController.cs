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

    /// <summary>
    /// Retorna todos os modelos cadastrados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetAll()
    {
        var modelos = await _modeloService.GetAllModelosAsync();
        return Ok(modelos);
    }

    /// <summary>
    /// Retorna um modelo específico pelo ID.
    /// </summary>
    /// <param name="id">ID do modelo</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Modelo>> GetById(int id)
    {
        var modelo = await _modeloService.GetModeloByIdAsync(id);
        if (modelo == null) return NotFound();
        return Ok(modelo);
    }

    /// <summary>
    /// Retorna os modelos de um determinado fabricante.
    /// </summary>
    /// <param name="fabricanteId">ID do fabricante</param>
    [HttpGet("por-fabricante/{fabricanteId}")]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetPorFabricante(int fabricanteId)
    {
        var modelos = await _modeloService.GetModelosPorFabricanteAsync(fabricanteId);
        return Ok(modelos);
    }

    /// <summary>
    /// Retorna os modelos de um determinado tipo.
    /// </summary>
    /// <param name="tipo">Tipo do modelo (ex: SUV, Sedan, Hatch)</param>
    [HttpGet("por-tipo/{tipo}")]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetPorTipo(string tipo)
    {
        var modelos = await _modeloService.GetModelosPorTipoAsync(tipo);
        return Ok(modelos);
    }

    /// <summary>
    /// Cria um novo modelo.
    /// </summary>
    /// <param name="modelo">Objeto Modelo</param>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Modelo modelo)
    {
        await _modeloService.CreateModeloAsync(modelo);
        return CreatedAtAction(nameof(GetById), new { id = modelo.Id }, modelo);
    }

    /// <summary>
    /// Atualiza os dados de um modelo existente.
    /// </summary>
    /// <param name="id">ID do modelo</param>
    /// <param name="modelo">Objeto Modelo com os dados atualizados</param>
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

    /// <summary>
    /// Exclui um modelo pelo ID.
    /// </summary>
    /// <param name="id">ID do modelo</param>
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
