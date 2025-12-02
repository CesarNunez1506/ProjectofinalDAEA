using Application.DTOs.Sales;
using Application.UseCases.Sales.Sales.Commands;
using Application.UseCases.Sales.Sales.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/sales")]
[Produces("application/json")]
public class SalesController : ControllerBase
{
    private readonly CreateSaleCommand _createCommand;
    private readonly UpdateSaleCommand _updateCommand;
    private readonly DeleteSaleCommand _deleteCommand;
    private readonly GetAllSalesQuery _getAllQuery;
    private readonly GetSaleByIdQuery _getByIdQuery;

    public SalesController(
        CreateSaleCommand createCommand,
        UpdateSaleCommand updateCommand,
        DeleteSaleCommand deleteCommand,
        GetAllSalesQuery getAllQuery,
        GetSaleByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto dto)
    {
        try
        {
            var result = await _createCommand.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
            return NotFound(new { message = $"Venta con ID {id} no encontrada" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleDto dto)
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
            return Ok(new { message = "Venta eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
