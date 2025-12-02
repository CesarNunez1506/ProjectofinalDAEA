using Application.DTOs.Sales;
using Application.UseCases.Sales.SalesChannels.Commands;
using Application.UseCases.Sales.SalesChannels.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/sales_channel")]
[Produces("application/json")]
public class SalesChannelsController : ControllerBase
{
    private readonly CreateSalesChannelCommand _createCommand;
    private readonly UpdateSalesChannelCommand _updateCommand;
    private readonly DeleteSalesChannelCommand _deleteCommand;
    private readonly GetAllSalesChannelsQuery _getAllQuery;
    private readonly GetSalesChannelByIdQuery _getByIdQuery;

    public SalesChannelsController(
        CreateSalesChannelCommand createCommand,
        UpdateSalesChannelCommand updateCommand,
        DeleteSalesChannelCommand deleteCommand,
        GetAllSalesChannelsQuery getAllQuery,
        GetSalesChannelByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSalesChannelDto dto)
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
            return NotFound(new { message = $"Canal de ventas con ID {id} no encontrado" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSalesChannelDto dto)
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
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteCommand.ExecuteAsync(id);
            return Ok(new { message = "Canal de ventas eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
