using Application.DTOs.Production;
using Application.UseCases.Production.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.ProductionModule;

/// <summary>
/// Controlador para gestión de categorías de productos
/// </summary>
[ApiController]
[Route("api/production/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryUseCase _createUseCase;
    private readonly GetAllCategoriesUseCase _getAllUseCase;
    private readonly GetCategoryByIdUseCase _getByIdUseCase;
    private readonly UpdateCategoryUseCase _updateUseCase;
    private readonly DeleteCategoryUseCase _deleteUseCase;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        CreateCategoryUseCase createUseCase,
        GetAllCategoriesUseCase getAllUseCase,
        GetCategoryByIdUseCase getByIdUseCase,
        UpdateCategoryUseCase updateUseCase,
        DeleteCategoryUseCase deleteUseCase,
        ILogger<CategoriesController> logger)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las categorías
    /// </summary>
    /// <returns>Lista de categorías</returns>
    /// <response code="200">Retorna la lista de categorías</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        try
        {
            var categories = await _getAllUseCase.ExecuteAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener categorías");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una categoría por su ID
    /// </summary>
    /// <param name="id">ID de la categoría</param>
    /// <returns>Categoría solicitada</returns>
    /// <response code="200">Retorna la categoría</response>
    /// <response code="404">Categoría no encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id)
    {
        try
        {
            var category = await _getByIdUseCase.ExecuteAsync(id);
            
            if (category == null)
                return NotFound(new { error = $"Categoría con ID {id} no encontrada" });

            return Ok(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener categoría {CategoryId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva categoría
    /// </summary>
    /// <param name="dto">Datos de la categoría a crear</param>
    /// <returns>Categoría creada</returns>
    /// <response code="201">Categoría creada exitosamente</response>
    /// <response code="400">Datos inválidos o categoría duplicada</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _createUseCase.ExecuteAsync(dto);
            
            return CreatedAtAction(
                nameof(GetById), 
                new { id = category.Id }, 
                category);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de validación al crear categoría");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear categoría");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una categoría existente
    /// </summary>
    /// <param name="id">ID de la categoría</param>
    /// <param name="dto">Datos a actualizar</param>
    /// <returns>Categoría actualizada</returns>
    /// <response code="200">Categoría actualizada exitosamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="404">Categoría no encontrada</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(category);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Categoría no encontrada: {CategoryId}", id);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de validación al actualizar categoría");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar categoría {CategoryId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (soft delete) una categoría
    /// </summary>
    /// <param name="id">ID de la categoría</param>
    /// <returns>Resultado de la eliminación</returns>
    /// <response code="200">Categoría eliminada exitosamente</response>
    /// <response code="400">No se puede eliminar (tiene productos activos)</response>
    /// <response code="404">Categoría no encontrada</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _deleteUseCase.ExecuteAsync(id);
            return Ok(new { message = "Categoría eliminada exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Categoría no encontrada: {CategoryId}", id);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "No se puede eliminar categoría {CategoryId}", id);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar categoría {CategoryId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
