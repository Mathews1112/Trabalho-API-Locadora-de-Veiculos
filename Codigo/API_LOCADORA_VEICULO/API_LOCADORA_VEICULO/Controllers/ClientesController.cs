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

    /// <summary>
    /// Retorna todos os clientes cadastrados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await _clienteService.GetAllClientesAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Retorna um cliente pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    /// <summary>
    /// Retorna um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente</param>
    [HttpGet("por-cpf/{cpf}")]
    public async Task<ActionResult<Cliente>> GetByCpf(string cpf)
    {
        var cliente = await _clienteService.GetClienteByCpfAsync(cpf);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <param name="cliente">Objeto Cliente</param>
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

    /// <summary>
    /// Atualiza um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="cliente">Objeto Cliente atualizado</param>
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

    /// <summary>
    /// Exclui um cliente pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente</param>
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

    /// <summary>
    /// Retorna todos os clientes ativos.
    /// </summary>
    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAtivos()
    {
        var clientes = await _clienteService.GetClientesAtivosAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Retorna os clientes que possuem aluguel ativo.
    /// </summary>
    [HttpGet("com-aluguel-ativo")]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetComAluguelAtivo()
    {
        var clientes = await _clienteService.GetClientesComAluguelAtivoAsync();
        return Ok(clientes);
    }
}
