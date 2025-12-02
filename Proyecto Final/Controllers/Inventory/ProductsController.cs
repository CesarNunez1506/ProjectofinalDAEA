using Application.DTOs.Inventory;
using Application.UseCases.Inventory.Commands;
using Application.UseCases.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Inventory;

[ApiController]
[Route("api/inventory/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener todos los productos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    /// <summary>
    /// Obtener producto por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
        {
            return NotFound(new { message = "Producto no encontrado" });
        }
        return Ok(product);
    }

    /// <summary>
    /// Obtener productos activos
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetActive()
    {
        var products = await _mediator.Send(new GetActiveProductsQuery());
        return Ok(products);
    }

    /// <summary>
    /// Obtener productos por categoría
    /// </summary>
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(Guid categoryId)
    {
        var products = await _mediator.Send(new GetProductsByCategoryQuery(categoryId));
        return Ok(products);
    }

    /// <summary>
    /// Buscar productos por nombre
    /// </summary>
    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> SearchByName(string name)
    {
        var products = await _mediator.Send(new SearchProductsByNameQuery(name));
        return Ok(products);
    }

    /// <summary>
    /// Obtener productos con stock bajo
    /// </summary>
    [HttpGet("low-stock")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStock([FromQuery] int minQuantity = 10)
    {
        var products = await _mediator.Send(new GetLowStockProductsQuery(minQuantity));
        return Ok(products);
    }

    /// <summary>
    /// Obtener productos producibles
    /// </summary>
    [HttpGet("producible")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducible()
    {
        var products = await _mediator.Send(new GetProducibleProductsQuery());
        return Ok(products);
    }

    /// <summary>
    /// Crear un nuevo producto
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        try
        {
            var product = await _mediator.Send(new CreateProductCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualizar un producto
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        try
        {
            if (dto.Id != id)
            {
                dto.Id = id;
            }
            var product = await _mediator.Send(new UpdateProductCommand(dto));
            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Eliminar un producto (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
