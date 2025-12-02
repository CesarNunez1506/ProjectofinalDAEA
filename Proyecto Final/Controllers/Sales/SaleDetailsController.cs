using Application.DTOs.Sales;
using Application.UseCases.Sales.SaleDetails.Commands;
using Application.UseCases.Sales.SaleDetails.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/saleDetail")]
[Produces("application/json")]
public class SaleDetailsController : ControllerBase
{
    private readonly CreateSaleDetailCommand _createCommand;
    private readonly UpdateSaleDetailCommand _updateCommand;
    private readonly DeleteSaleDetailCommand _deleteCommand;
    private readonly GetAllSaleDetailsQuery _getAllQuery;
    private readonly GetSaleDetailByIdQuery _getByIdQuery;

    public SaleDetailsController(
        CreateSaleDetailCommand createCommand,
        UpdateSaleDetailCommand updateCommand,
        DeleteSaleDetailCommand deleteCommand,
        GetAllSaleDetailsQuery getAllQuery,
        GetSaleDetailByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDetailDto dto)
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
            return NotFound(new { message = $"Detalle de venta con ID {id} no encontrado" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleDetailDto dto)
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
            return Ok(new { message = "Detalle de venta eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
