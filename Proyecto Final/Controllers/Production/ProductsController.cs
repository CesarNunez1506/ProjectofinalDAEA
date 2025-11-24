using Application.DTOs.Production;
using Application.UseCases.Production.Products;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Production;

/// <summary>
/// Controlador para la gestión de productos
/// </summary>
[ApiController]
[Route("api/production/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _createProductUseCase;
    private readonly GetAllProductsUseCase _getAllProductsUseCase;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;
    private readonly UpdateProductUseCase _updateProductUseCase;
    private readonly DeleteProductUseCase _deleteProductUseCase;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        CreateProductUseCase createProductUseCase,
        GetAllProductsUseCase getAllProductsUseCase,
        GetProductByIdUseCase getProductByIdUseCase,
        UpdateProductUseCase updateProductUseCase,
        DeleteProductUseCase deleteProductUseCase,
        ILogger<ProductsController> logger)
    {
        _createProductUseCase = createProductUseCase;
        _getAllProductsUseCase = getAllProductsUseCase;
        _getProductByIdUseCase = getProductByIdUseCase;
        _updateProductUseCase = updateProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los productos con sus categorías
    /// </summary>
    /// <returns>Lista de productos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _getAllProductsUseCase.ExecuteAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un producto por ID
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <returns>Producto encontrado</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var product = await _getProductByIdUseCase.ExecuteAsync(id);
            
            if (product == null)
                return NotFound(new { message = $"No se encontró el producto con ID {id}" });

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener producto {ProductId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea un nuevo producto con imagen opcional
    /// </summary>
    /// <param name="dto">Datos del producto (multipart/form-data)</param>
    /// <returns>Producto creado</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _createProductUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producto");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un producto existente
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <param name="dto">Datos a actualizar (multipart/form-data)</param>
    /// <returns>Producto actualizado</returns>
    [HttpPatch("{id}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateProductDto dto)
    {
        try
        {
            var product = await _updateProductUseCase.ExecuteAsync(id, dto);
            return Ok(product);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar producto {ProductId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina (soft delete) un producto
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteProductUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar producto {ProductId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
