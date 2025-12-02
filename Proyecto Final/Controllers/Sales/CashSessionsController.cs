using Application.DTOs.Sales;
using Application.UseCases.Sales.CashSessions.Commands;
using Application.UseCases.Sales.CashSessions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/cash_session")]
[Produces("application/json")]
public class CashSessionsController : ControllerBase
{
    private readonly CreateCashSessionCommand _createCommand;
    private readonly UpdateCashSessionCommand _updateCommand;
    private readonly DeleteCashSessionCommand _deleteCommand;
    private readonly GetAllCashSessionsQuery _getAllQuery;
    private readonly GetCashSessionByIdQuery _getByIdQuery;

    public CashSessionsController(
        CreateCashSessionCommand createCommand,
        UpdateCashSessionCommand updateCommand,
        DeleteCashSessionCommand deleteCommand,
        GetAllCashSessionsQuery getAllQuery,
        GetCashSessionByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCashSessionDto dto)
    {
        try
        {
            // TODO: Obtener userId del token/claims actual
            var userId = Guid.NewGuid().ToString(); // Temporal
            var result = await _createCommand.ExecuteAsync(dto, userId);
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
            return NotFound(new { message = $"Sesión de caja con ID {id} no encontrada" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCashSessionDto dto)
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
            return Ok(new { message = "Sesión de caja eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
