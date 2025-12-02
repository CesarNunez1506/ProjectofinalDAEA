using Application.DTOs.Sales;
using Application.UseCases.Sales.Returns.Commands;
using Application.UseCases.Sales.Returns.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Sales;

[ApiController]
[Route("api/returns")]
[Produces("application/json")]
public class ReturnsController : ControllerBase
{
    private readonly CreateReturnCommand _createCommand;
    private readonly UpdateReturnCommand _updateCommand;
    private readonly DeleteReturnCommand _deleteCommand;
    private readonly GetAllReturnsQuery _getAllQuery;
    private readonly GetReturnByIdQuery _getByIdQuery;

    public ReturnsController(
        CreateReturnCommand createCommand,
        UpdateReturnCommand updateCommand,
        DeleteReturnCommand deleteCommand,
        GetAllReturnsQuery getAllQuery,
        GetReturnByIdQuery getByIdQuery)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReturnDto dto)
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
            return NotFound(new { message = $"Devolución con ID {id} no encontrada" });
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReturnDto dto)
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
            return Ok(new { message = "Devolución eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
