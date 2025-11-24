using Application.DTOs.Production;
using Application.UseCases.Production.Losts;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.ProductionModule;

/// <summary>
/// Controlador para la gestión de pérdidas en producción
/// </summary>
[ApiController]
[Route("api/production/[controller]")]
public class LostsController : ControllerBase
{
    private readonly CreateLostUseCase _createLostUseCase;
    private readonly GetAllLostsUseCase _getAllLostsUseCase;
    private readonly GetLostByIdUseCase _getLostByIdUseCase;
    private readonly UpdateLostUseCase _updateLostUseCase;
    private readonly DeleteLostUseCase _deleteLostUseCase;
    private readonly ILogger<LostsController> _logger;

    public LostsController(
        CreateLostUseCase createLostUseCase,
        GetAllLostsUseCase getAllLostsUseCase,
        GetLostByIdUseCase getLostByIdUseCase,
        UpdateLostUseCase updateLostUseCase,
        DeleteLostUseCase deleteLostUseCase,
        ILogger<LostsController> logger)
    {
        _createLostUseCase = createLostUseCase;
        _getAllLostsUseCase = getAllLostsUseCase;
        _getLostByIdUseCase = getLostByIdUseCase;
        _updateLostUseCase = updateLostUseCase;
        _deleteLostUseCase = deleteLostUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las pérdidas con sus relaciones
    /// </summary>
    /// <returns>Lista de pérdidas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LostDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var losts = await _getAllLostsUseCase.ExecuteAsync();
            return Ok(losts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pérdidas");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una pérdida por ID
    /// </summary>
    /// <param name="id">ID de la pérdida</param>
    /// <returns>Pérdida encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var lost = await _getLostByIdUseCase.ExecuteAsync(id);
            
            if (lost == null)
                return NotFound(new { message = $"No se encontró la pérdida con ID {id}" });

            return Ok(lost);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pérdida {LostId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva pérdida
    /// </summary>
    /// <param name="dto">Datos de la pérdida</param>
    /// <returns>Pérdida creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(LostDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateLostDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lost = await _createLostUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = lost.Id }, lost);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear pérdida");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una pérdida existente
    /// </summary>
    /// <param name="id">ID de la pérdida</param>
    /// <param name="dto">Datos a actualizar</param>
    /// <returns>Pérdida actualizada</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(LostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLostDto dto)
    {
        try
        {
            var lost = await _updateLostUseCase.ExecuteAsync(id, dto);
            return Ok(lost);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar pérdida {LostId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (hard delete) una pérdida
    /// </summary>
    /// <param name="id">ID de la pérdida</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteLostUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar pérdida {LostId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
