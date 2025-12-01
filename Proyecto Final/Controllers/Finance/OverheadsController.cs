using Application.DTOs.Finance;
using Application.DTOs.Finance.Request;
using Application.UseCases.Finance.Commands;
using Application.UseCases.Finance.Overheads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Proyecto_Final.Controllers.Finance;

/// <summary>
/// Controlador para gestión de overheads (cierres de gastos del monasterio)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OverheadsController : ControllerBase
{
    private readonly CreateOverheadUseCase _createUseCase;
    private readonly GetAllOverheadsUseCase _getAllUseCase;
    private readonly GetOverheadByIdUseCase _getByIdUseCase;
    private readonly UpdateOverheadUseCase _updateUseCase;
    private readonly DeleteOverheadUseCase _deleteUseCase;
    private readonly GetExpensesByDateRangeUseCase _getByDateRangeUseCase;
    private readonly ILogger<OverheadsController> _logger;

    public OverheadsController(
        CreateOverheadUseCase createUseCase,
        GetAllOverheadsUseCase getAllUseCase,
        GetOverheadByIdUseCase getByIdUseCase,
        UpdateOverheadUseCase updateUseCase,
        DeleteOverheadUseCase deleteUseCase,
        GetExpensesByDateRangeUseCase getByDateRangeUseCase,
        ILogger<OverheadsController> logger)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _getByDateRangeUseCase = getByDateRangeUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los overheads del monasterio
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OverheadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OverheadDto>>> GetAll()
    {
        try
        {
            var overheads = await _getAllUseCase.ExecuteAsync();
            return Ok(overheads);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener overheads");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un overhead por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OverheadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OverheadDto>> GetById(Guid id)
    {
        try
        {
            var overhead = await _getByIdUseCase.ExecuteAsync(id);

            if (overhead == null)
                return NotFound(new { error = $"Overhead con ID {id} no encontrado" });

            return Ok(overhead);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener overhead {OverheadId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea un nuevo overhead (cierre de gastos del monasterio)
    /// Asocia automáticamente los gastos sin asignar
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OverheadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OverheadDto>> Create([FromBody] CreateOverheadDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var overhead = await _createUseCase.ExecuteAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = overhead.Id },
                overhead);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear overhead");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un overhead
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(OverheadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OverheadDto>> Update(Guid id, [FromBody] UpdateOverheadDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var overhead = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(overhead);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar overhead {OverheadId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un overhead
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar overhead {OverheadId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene gastos del monasterio por rango de fechas
    /// </summary>
    [HttpGet("expenses")]
    [ProducesResponseType(typeof(IEnumerable<MonasteryExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetExpensesByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            if (startDate > endDate)
                return BadRequest(new { error = "La fecha de inicio debe ser menor o igual a la fecha de fin" });

            var expenses = await _getByDateRangeUseCase.ExecuteAsync(startDate, endDate);

            return Ok(new
            {
                success = true,
                message = "Datos de gastos del Monasterio obtenidos correctamente",
                data = expenses
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener gastos por rango de fechas");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
