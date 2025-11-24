using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear un nuevo registro de pérdida
/// </summary>
public class CreateLostDto
{
    [Required(ErrorMessage = "El ID de la producción es requerido")]
    public Guid ProductionId { get; set; }
    
    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "El tipo de pérdida es requerido")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El tipo de pérdida debe tener entre 1 y 50 caracteres")]
    public string LostType { get; set; } = string.Empty;
    
    [StringLength(255, ErrorMessage = "Las observaciones no deben exceder los 255 caracteres")]
    public string? Observations { get; set; }
}

/// <summary>
/// DTO para actualizar un registro de pérdida existente
/// </summary>
public class UpdateLostDto
{
    public Guid? ProductionId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
    public int? Quantity { get; set; }
    
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El tipo de pérdida debe tener entre 1 y 50 caracteres")]
    public string? LostType { get; set; }
    
    [StringLength(255, ErrorMessage = "Las observaciones no deben exceder los 255 caracteres")]
    public string? Observations { get; set; }
}

/// <summary>
/// DTO de respuesta para pérdida
/// </summary>
public class LostDto
{
    public Guid Id { get; set; }
    public Guid ProductionId { get; set; }
    public int Quantity { get; set; }
    public string LostType { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Información de la producción (opcional, según include)
    /// </summary>
    public ProductionDto? Production { get; set; }
}
