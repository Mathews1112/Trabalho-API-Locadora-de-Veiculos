using LocadoraVeiculos.Models.Entities;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await _clienteService.GetAllClientesAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpGet("por-cpf/{cpf}")]
    public async Task<ActionResult<Cliente>> GetByCpf(string cpf)
    {
        var cliente = await _clienteService.GetClienteByCpfAsync(cpf);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Cliente cliente)
    {
        try
        {
            await _clienteService.CreateClienteAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();

        try
        {
            await _clienteService.UpdateClienteAsync(cliente);
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
            await _clienteService.DeleteClienteAsync(id);
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

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAtivos()
    {
        var clientes = await _clienteService.GetClientesAtivosAsync();
        return Ok(clientes);
    }

    [HttpGet("com-aluguel-ativo")]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetComAluguelAtivo()
    {
        var clientes = await _clienteService.GetClientesComAluguelAtivoAsync();
        return Ok(clientes);
    }
}