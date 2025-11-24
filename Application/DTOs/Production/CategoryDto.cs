using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear una nueva categoría
/// </summary>
public class CreateCategoryDto
{
    [Required(ErrorMessage = "El nombre de la categoría es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO para actualizar una categoría existente
/// </summary>
public class UpdateCategoryDto
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string? Name { get; set; }
    
    [StringLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres")]
    public string? Description { get; set; }
    
    public bool? Status { get; set; }
}

/// <summary>
/// DTO de respuesta para categoría
/// </summary>
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
