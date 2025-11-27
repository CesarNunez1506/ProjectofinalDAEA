using Application.DTOs.Production;
using Application.UseCases.Production.Productions;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.ProductionModule;

/// <summary>
/// Controlador para la gestión de producciones
/// </summary>
[ApiController]
[Route("api/production/productions")]
public class ProductionsController : ControllerBase
{
    private readonly CreateProductionUseCase _createProductionUseCase;
    private readonly GetAllProductionsUseCase _getAllProductionsUseCase;
    private readonly GetProductionByIdUseCase _getProductionByIdUseCase;
    private readonly UpdateProductionUseCase _updateProductionUseCase;
    private readonly ToggleProductionStatusUseCase _toggleProductionStatusUseCase;
    private readonly ILogger<ProductionsController> _logger;

    public ProductionsController(
        CreateProductionUseCase createProductionUseCase,
        GetAllProductionsUseCase getAllProductionsUseCase,
        GetProductionByIdUseCase getProductionByIdUseCase,
        UpdateProductionUseCase updateProductionUseCase,
        ToggleProductionStatusUseCase toggleProductionStatusUseCase,
        ILogger<ProductionsController> logger)
    {
        _createProductionUseCase = createProductionUseCase;
        _getAllProductionsUseCase = getAllProductionsUseCase;
        _getProductionByIdUseCase = getProductionByIdUseCase;
        _updateProductionUseCase = updateProductionUseCase;
        _toggleProductionStatusUseCase = toggleProductionStatusUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las producciones
    /// </summary>
    /// <returns>Lista de producciones</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var productions = await _getAllProductionsUseCase.ExecuteAsync();
            return Ok(productions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producciones");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una producción por ID
    /// </summary>
    /// <param name="id">ID de la producción</param>
    /// <returns>Producción encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var production = await _getProductionByIdUseCase.ExecuteAsync(id);
            
            if (production == null)
                return NotFound(new { message = $"No se encontró la producción con ID {id}" });

            return Ok(production);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producción {ProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva producción con procesamiento FIFO de recursos
    /// </summary>
    /// <param name="dto">Datos de la producción</param>
    /// <returns>Producción creada con detalles de movimientos</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductionCreatedResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductionDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _createProductionUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producción");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una producción existente
    /// </summary>
    /// <param name="id">ID de la producción</param>
    /// <param name="dto">Datos a actualizar</param>
    /// <returns>Producción actualizada</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(ProductionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductionDto dto)
    {
        try
        {
            var production = await _updateProductionUseCase.ExecuteAsync(id, dto);
            return Ok(production);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar producción {ProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Activa/desactiva una producción
    /// </summary>
    /// <param name="id">ID de la producción</param>
    /// <returns>Resultado de la operación</returns>
    [HttpPatch("{id}/toggle-status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        try
        {
            await _toggleProductionStatusUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar estado de producción {ProductionId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
