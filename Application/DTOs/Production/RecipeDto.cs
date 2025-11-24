using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear una nueva receta
/// </summary>
public class CreateRecipeDto
{
    [Required(ErrorMessage = "El ID del producto es requerido")]
    public Guid ProductId { get; set; }
    
    [Required(ErrorMessage = "El ID del recurso es requerido")]
    public Guid ResourceId { get; set; }
    
    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(0.01, 1000000, ErrorMessage = "La cantidad debe estar entre 0.01 y 1,000,000")]
    public double Quantity { get; set; }
    
    [Required(ErrorMessage = "La unidad es requerida")]
    [RegularExpression("^(g|kg|ml|l|unidades)$", ErrorMessage = "La unidad debe ser: g, kg, ml, l o unidades")]
    public string Unit { get; set; } = string.Empty;
}

/// <summary>
/// DTO para actualizar una receta existente (solo quantity y unit)
/// </summary>
public class UpdateRecipeDto
{
    [Range(0.01, 1000000, ErrorMessage = "La cantidad debe estar entre 0.01 y 1,000,000")]
    public double? Quantity { get; set; }
    
    [RegularExpression("^(g|kg|ml|l|unidades)$", ErrorMessage = "La unidad debe ser: g, kg, ml, l o unidades")]
    public string? Unit { get; set; }
}

/// <summary>
/// DTO de respuesta para receta
/// </summary>
public class RecipeDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ResourceId { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Información del producto (opcional, según include)
    /// </summary>
    public ProductDto? Product { get; set; }
    
    /// <summary>
    /// Información del recurso (opcional, según include)
    /// </summary>
    public ResourceDto? Resource { get; set; }
}

/// <summary>
/// DTO simple para recurso (usado en recetas)
/// </summary>
public class ResourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Observation { get; set; }
    public bool Status { get; set; }
}
