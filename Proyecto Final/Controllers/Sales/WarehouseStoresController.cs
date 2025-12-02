using Application.DTOs.Sales;
using Application.UseCases.Sales.WarehouseStores.Commands;
using Application.UseCases.Sales.WarehouseStores.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/warehouseStores")]
[Produces("application/json")]
public class WarehouseStoresController : ControllerBase
{
    private readonly CreateWarehouseStoreCommand _createCommand;
    private readonly UpdateWarehouseStoreCommand _updateCommand;
    private readonly DeleteWarehouseStoreCommand _deleteCommand;
    private readonly GetAllWarehouseStoresQuery _getAllQuery;
    private readonly GetWarehouseStoreByIdQuery _getByIdQuery;

    public WarehouseStoresController(
        CreateWarehouseStoreCommand createCommand,
        UpdateWarehouseStoreCommand updateCommand,
        DeleteWarehouseStoreCommand deleteCommand,
        GetAllWarehouseStoresQuery getAllQuery,
        GetWarehouseStoreByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseStoreDto dto)
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllQuery.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getByIdQuery.ExecuteAsync(id);
        if (result == null)
            return NotFound(new { message = $"Inventario con ID {id} no encontrado" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseStoreDto dto)
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
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteCommand.ExecuteAsync(id);
            return Ok(new { message = "Inventario eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
