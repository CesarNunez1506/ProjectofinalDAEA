using Application.DTOs.Sales;
using Application.UseCases.Sales.Stores.Commands;
using Application.UseCases.Sales.Stores.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/store")]
[Produces("application/json")]
public class StoresController : ControllerBase
{
    private readonly CreateStoreCommand _createCommand;
    private readonly UpdateStoreCommand _updateCommand;
    private readonly DeleteStoreCommand _deleteCommand;
    private readonly GetAllStoresQuery _getAllQuery;
    private readonly GetStoreByIdQuery _getByIdQuery;
    private readonly ILogger<StoresController> _logger;

    public StoresController(
        CreateStoreCommand createCommand,
        UpdateStoreCommand updateCommand,
        DeleteStoreCommand deleteCommand,
        GetAllStoresQuery getAllQuery,
        GetStoreByIdQuery getByIdQuery,
        ILogger<StoresController> logger)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StoreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateStoreDto dto)
    {
        try
        {
            var result = await _createCommand.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear tienda");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StoreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllQuery.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StoreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getByIdQuery.ExecuteAsync(id);
        if (result == null)
            return NotFound(new { message = $"Tienda con ID {id} no encontrada" });

        return Ok(result);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(StoreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStoreDto dto)
    {
        try
        {
            var result = await _updateCommand.ExecuteAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar tienda");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteCommand.ExecuteAsync(id);
            return Ok(new { message = "Tienda eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar tienda");
            return BadRequest(new { message = ex.Message });
        }
    }
}
