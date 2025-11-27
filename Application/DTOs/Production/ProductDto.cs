using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear un nuevo producto
/// </summary>
public class CreateProductDto
{
    [Required(ErrorMessage = "El nombre del producto es requerido")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 50 caracteres")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La categoría es requerida")]
    public Guid CategoryId { get; set; }
    
    [Required(ErrorMessage = "El precio es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public decimal Price { get; set; }
    
    [StringLength(2048, ErrorMessage = "La descripción no debe exceder los 2048 caracteres")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Archivo de imagen a cargar (opcional)
    /// </summary>
    public IFormFile? ImageFile { get; set; }
}

/// <summary>
/// DTO para actualizar un producto existente
/// </summary>
public class UpdateProductDto
{
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 50 caracteres")]
    public string? Name { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public decimal? Price { get; set; }
    
    [StringLength(2048, ErrorMessage = "La descripción no debe exceder los 2048 caracteres")]
    public string? Description { get; set; }
    
    public bool? Status { get; set; }
    
    /// <summary>
    /// Archivo de imagen a cargar (opcional)
    /// </summary>
    public IFormFile? ImageFile { get; set; }
}

/// <summary>
/// DTO de respuesta para producto
/// </summary>
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Status { get; set; }
    public bool Producible { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Categoría relacionada (opcional, según include)
    /// </summary>
    public CategoryDto? Category { get; set; }
}
