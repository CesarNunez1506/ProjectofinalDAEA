using Application.DTOs.Museum;
using Application.UseCases.Museum.Entrances.Commands;
using Application.UseCases.Museum.Entrances.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Museum;

// Controlador para gestionar las entradas del museo
[ApiController]
[Route("api/museum/entrances")]
[Tags("Museum")]
public class EntrancesController : ControllerBase
{
    private readonly CreateEntranceUseCase _createUseCase;
    private readonly UpdateEntranceUseCase _updateUseCase;
    private readonly DeleteEntranceUseCase _deleteUseCase;
    private readonly GetAllEntrancesUseCase _getAllUseCase;
    private readonly GetEntranceByIdUseCase _getByIdUseCase;

    public EntrancesController(
        CreateEntranceUseCase createUseCase,
        UpdateEntranceUseCase updateUseCase,
        DeleteEntranceUseCase deleteUseCase,
        GetAllEntrancesUseCase getAllUseCase,
        GetEntranceByIdUseCase getByIdUseCase)
    {
        _createUseCase = createUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
    }
    
    // Controlador para gestionar las entradas del museo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EntranceDto>>> GetAll()
    {
        try
        {
            var entrances = await _getAllUseCase.ExecuteAsync();
            return Ok(entrances);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las entradas", error = ex.Message });
        }
    }

    // Controlador para gestionar las entradas del museo
    [HttpGet("{id}")]
    public async Task<ActionResult<EntranceDto>> GetById(Guid id)
    {
        try
        {
            var entrance = await _getByIdUseCase.ExecuteAsync(id);
            return Ok(entrance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la entrada", error = ex.Message });
        }
    }

    // Controlador para gestionar las entradas del museo
    [HttpPost]
    public async Task<ActionResult<EntranceDto>> Create([FromBody] CreateEntranceDto dto)
    {
        try
        {
            var entrance = await _createUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = entrance.Id }, entrance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al crear la entrada", error = ex.Message });
        }
    }

    // Controlador para gestionar las entradas del museo
    [HttpPut("{id}")]
    public async Task<ActionResult<EntranceDto>> Update(Guid id, [FromBody] UpdateEntranceDto dto)
    {
        try
        {
            var entrance = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(entrance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al actualizar la entrada", error = ex.Message });
        }
    }

    // Controlador para gestionar las entradas del museo
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la entrada", error = ex.Message });
        }
    }
}
