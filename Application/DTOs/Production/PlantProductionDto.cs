using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear una nueva planta de producción
/// </summary>
public class CreatePlantProductionDto
{
    [Required(ErrorMessage = "El nombre de la planta es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string PlantName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "La dirección debe tener entre 1 y 255 caracteres")]
    public string Address { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El ID del almacén es requerido")]
    public Guid WarehouseId { get; set; }
}

/// <summary>
/// DTO para actualizar una planta de producción existente
/// </summary>
public class UpdatePlantProductionDto
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string? PlantName { get; set; }
    
    [StringLength(255, MinimumLength = 1, ErrorMessage = "La dirección debe tener entre 1 y 255 caracteres")]
    public string? Address { get; set; }
    
    public Guid? WarehouseId { get; set; }
    
    public bool? Status { get; set; }
}

/// <summary>
/// DTO de respuesta para planta de producción
/// </summary>
public class PlantProductionDto
{
    public Guid Id { get; set; }
    public string PlantName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid WarehouseId { get; set; }
    public bool Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Información del almacén (opcional, según include)
    /// </summary>
    public WarehouseDto? Warehouse { get; set; }
}

/// <summary>
/// DTO simple para almacén (usado en plantas)
/// </summary>
public class WarehouseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool Status { get; set; }
}
