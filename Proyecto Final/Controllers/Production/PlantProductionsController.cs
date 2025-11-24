using Application.DTOs.Production;
using Application.UseCases.Production.PlantProductions;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Production;

/// <summary>
/// Controlador para la gestión de producciones de planta (recursos)
/// </summary>
[ApiController]
[Route("api/production/plant-productions")]
public class PlantProductionsController : ControllerBase
{
    private readonly CreatePlantProductionUseCase _createPlantProductionUseCase;
    private readonly GetAllPlantProductionsUseCase _getAllPlantProductionsUseCase;
    private readonly GetPlantProductionByIdUseCase _getPlantProductionByIdUseCase;
    private readonly UpdatePlantProductionUseCase _updatePlantProductionUseCase;
    private readonly DeletePlantProductionUseCase _deletePlantProductionUseCase;
    private readonly ILogger<PlantProductionsController> _logger;

    public PlantProductionsController(
        CreatePlantProductionUseCase createPlantProductionUseCase,
        GetAllPlantProductionsUseCase getAllPlantProductionsUseCase,
        GetPlantProductionByIdUseCase getPlantProductionByIdUseCase,
        UpdatePlantProductionUseCase updatePlantProductionUseCase,
        DeletePlantProductionUseCase deletePlantProductionUseCase,
        ILogger<PlantProductionsController> logger)
    {
        _createPlantProductionUseCase = createPlantProductionUseCase;
        _getAllPlantProductionsUseCase = getAllPlantProductionsUseCase;
        _getPlantProductionByIdUseCase = getPlantProductionByIdUseCase;
        _updatePlantProductionUseCase = updatePlantProductionUseCase;
        _deletePlantProductionUseCase = deletePlantProductionUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las producciones de planta
    /// </summary>
    /// <returns>Lista de producciones de planta</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlantProductionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var plantProductions = await _getAllPlantProductionsUseCase.ExecuteAsync();
            return Ok(plantProductions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producciones de planta");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una producción de planta por ID
    /// </summary>
    /// <param name="id">ID de la producción de planta</param>
    /// <returns>Producción de planta encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var plantProduction = await _getPlantProductionByIdUseCase.ExecuteAsync(id);
            
            if (plantProduction == null)
                return NotFound(new { message = $"No se encontró la producción de planta con ID {id}" });

            return Ok(plantProduction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producción de planta {PlantProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva producción de planta
    /// </summary>
    /// <param name="dto">Datos de la producción de planta</param>
    /// <returns>Producción de planta creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePlantProductionDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plantProduction = await _createPlantProductionUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plantProduction.Id }, plantProduction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producción de planta");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una producción de planta existente
    /// </summary>
    /// <param name="id">ID de la producción de planta</param>
    /// <param name="dto">Datos a actualizar</param>
    /// <returns>Producción de planta actualizada</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlantProductionDto dto)
    {
        try
        {
            var plantProduction = await _updatePlantProductionUseCase.ExecuteAsync(id, dto);
            return Ok(plantProduction);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar producción de planta {PlantProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (soft delete) una producción de planta
    /// </summary>
    /// <param name="id">ID de la producción de planta</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deletePlantProductionUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar producción de planta {PlantProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
