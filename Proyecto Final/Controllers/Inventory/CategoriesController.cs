using Application.DTOs.Inventory;
using Application.UseCases.Inventory.Commands;
using Application.UseCases.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Inventory;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener todas las categorías
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    /// <summary>
    /// Obtener categoría por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (category == null)
        {
            return NotFound(new { message = "Categoría no encontrada" });
        }
        return Ok(category);
    }

    /// <summary>
    /// Obtener categorías activas
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetActive()
    {
        var categories = await _mediator.Send(new GetActiveCategoriesQuery());
        return Ok(categories);
    }

    /// <summary>
    /// Buscar categorías por nombre
    /// </summary>
    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> SearchByName(string name)
    {
        var categories = await _mediator.Send(new SearchCategoriesByNameQuery(name));
        return Ok(categories);
    }

    /// <summary>
    /// Crear una nueva categoría
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
    {
        try
        {
            var category = await _mediator.Send(new CreateCategoryCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualizar una categoría
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        try
        {
            dto.Id = id;
            var category = await _mediator.Send(new UpdateCategoryCommand(dto));
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Eliminar una categoría
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
