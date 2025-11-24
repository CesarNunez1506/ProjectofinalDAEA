using Application.DTOs.Production;
using Application.UseCases.Production.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Production;

/// <summary>
/// Controlador para la gestión de recetas de productos
/// </summary>
[ApiController]
[Route("api/production/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly CreateRecipeUseCase _createRecipeUseCase;
    private readonly GetAllRecipesUseCase _getAllRecipesUseCase;
    private readonly GetRecipesByProductIdUseCase _getRecipesByProductIdUseCase;
    private readonly UpdateRecipeUseCase _updateRecipeUseCase;
    private readonly DeleteRecipeUseCase _deleteRecipeUseCase;
    private readonly ILogger<RecipesController> _logger;

    public RecipesController(
        CreateRecipeUseCase createRecipeUseCase,
        GetAllRecipesUseCase getAllRecipesUseCase,
        GetRecipesByProductIdUseCase getRecipesByProductIdUseCase,
        UpdateRecipeUseCase updateRecipeUseCase,
        DeleteRecipeUseCase deleteRecipeUseCase,
        ILogger<RecipesController> logger)
    {
        _createRecipeUseCase = createRecipeUseCase;
        _getAllRecipesUseCase = getAllRecipesUseCase;
        _getRecipesByProductIdUseCase = getRecipesByProductIdUseCase;
        _updateRecipeUseCase = updateRecipeUseCase;
        _deleteRecipeUseCase = deleteRecipeUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las recetas con sus relaciones
    /// </summary>
    /// <returns>Lista de recetas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var recipes = await _getAllRecipesUseCase.ExecuteAsync();
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener recetas");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene las recetas de un producto específico
    /// </summary>
    /// <param name="productId">ID del producto</param>
    /// <returns>Lista de recetas del producto</returns>
    [HttpGet("product/{productId}")]
    [ProducesResponseType(typeof(IEnumerable<RecipeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        try
        {
            var recipes = await _getRecipesByProductIdUseCase.ExecuteAsync(productId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener recetas del producto {ProductId}", productId);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva receta
    /// </summary>
    /// <param name="dto">Datos de la receta</param>
    /// <returns>Receta creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipe = await _createRecipeUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetByProductId), new { productId = recipe.ProductId }, recipe);
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
            _logger.LogError(ex, "Error al crear receta");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una receta existente (solo cantidad y unidad)
    /// </summary>
    /// <param name="id">ID de la receta</param>
    /// <param name="dto">Datos a actualizar</param>
    /// <returns>Receta actualizada</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRecipeDto dto)
    {
        try
        {
            var recipe = await _updateRecipeUseCase.ExecuteAsync(id, dto);
            return Ok(recipe);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar receta {RecipeId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (hard delete) una receta
    /// </summary>
    /// <param name="id">ID de la receta</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteRecipeUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar receta {RecipeId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
