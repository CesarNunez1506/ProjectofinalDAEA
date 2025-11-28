using Application.DTOs.Production;
using Application.UseCases.Production.PlantProductions;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.ProductionModule;

/// <summary>
/// Controlador para gestión de plantas de producción
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PlantProductionsController : ControllerBase
{
    private readonly CreatePlantProductionUseCase _createUseCase;
    private readonly GetAllPlantProductionsUseCase _getAllUseCase;
    private readonly GetPlantProductionByIdUseCase _getByIdUseCase;
    private readonly UpdatePlantProductionUseCase _updateUseCase;
    private readonly DeletePlantProductionUseCase _deleteUseCase;
    private readonly ILogger<PlantProductionsController> _logger;

    public PlantProductionsController(
        CreatePlantProductionUseCase createUseCase,
        GetAllPlantProductionsUseCase getAllUseCase,
        GetPlantProductionByIdUseCase getByIdUseCase,
        UpdatePlantProductionUseCase updateUseCase,
        DeletePlantProductionUseCase deleteUseCase,
        ILogger<PlantProductionsController> logger)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las plantas de producción
    /// </summary>
    /// <returns>Lista de plantas de producción</returns>
    /// <response code="200">Retorna la lista de plantas</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlantProductionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PlantProductionDto>>> GetAll()
    {
        try
        {
            var plants = await _getAllUseCase.ExecuteAsync();
            return Ok(plants);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener plantas de producción");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una planta de producción por su ID
    /// </summary>
    /// <param name="id">ID de la planta</param>
    /// <returns>Planta solicitada</returns>
    /// <response code="200">Retorna la planta</response>
    /// <response code="404">Planta no encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlantProductionDto>> GetById(Guid id)
    {
        try
        {
            var plant = await _getByIdUseCase.ExecuteAsync(id);
            
            if (plant == null)
                return NotFound(new { error = $"Planta de producción con ID {id} no encontrada" });

            return Ok(plant);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener planta de producción {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva planta de producción
    /// </summary>
    /// <param name="dto">Datos de la nueva planta</param>
    /// <returns>Planta creada</returns>
    /// <response code="201">Planta creada exitosamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="409">Ya existe una planta con ese nombre</response>
    [HttpPost]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlantProductionDto>> Create([FromBody] CreatePlantProductionDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plant = await _createUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plant.Id }, plant);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Conflicto al crear planta: {Message}", ex.Message);
            return Conflict(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Almacén no encontrado: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear planta de producción");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una planta de producción existente
    /// </summary>
    /// <param name="id">ID de la planta a actualizar</param>
    /// <param name="dto">Datos actualizados de la planta</param>
    /// <returns>Planta actualizada</returns>
    /// <response code="200">Planta actualizada exitosamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="404">Planta no encontrada</response>
    /// <response code="409">Ya existe una planta con ese nombre</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PlantProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlantProductionDto>> Update(Guid id, [FromBody] UpdatePlantProductionDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plant = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(plant);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Planta o almacén no encontrado: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Conflicto al actualizar planta: {Message}", ex.Message);
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar planta de producción {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (soft delete) una planta de producción
    /// </summary>
    /// <param name="id">ID de la planta a eliminar</param>
    /// <returns>Confirmación de eliminación</returns>
    /// <response code="200">Planta eliminada exitosamente</response>
    /// <response code="404">Planta no encontrada</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _deleteUseCase.ExecuteAsync(id);
            return Ok(new { message = "Planta de producción eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Planta no encontrada: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar planta de producción {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
