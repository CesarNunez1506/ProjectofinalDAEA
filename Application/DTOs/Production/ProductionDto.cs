using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Production;

/// <summary>
/// DTO para crear un nuevo registro de producción
/// </summary>
public class CreateProductionDto
{
    [Required(ErrorMessage = "El ID del producto es requerido")]
    public Guid ProductId { get; set; }
    
    [Required(ErrorMessage = "La cantidad producida es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad producida debe ser mayor que 0")]
    public int QuantityProduced { get; set; }
    
    [Required(ErrorMessage = "La fecha de producción es requerida")]
    public DateTime ProductionDate { get; set; }
    
    [StringLength(255, ErrorMessage = "La observación no debe exceder los 255 caracteres")]
    public string? Observation { get; set; }
    
    [Required(ErrorMessage = "El ID de la planta es requerido")]
    public Guid PlantId { get; set; }
}

/// <summary>
/// DTO para actualizar un registro de producción existente
/// </summary>
public class UpdateProductionDto
{
    public Guid? ProductId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad producida debe ser mayor que 0")]
    public int? QuantityProduced { get; set; }
    
    public DateTime? ProductionDate { get; set; }
    
    [StringLength(255, ErrorMessage = "La observación no debe exceder los 255 caracteres")]
    public string? Observation { get; set; }
    
    public Guid? PlantId { get; set; }
}

/// <summary>
/// DTO de respuesta para producción
/// </summary>
public class ProductionDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int QuantityProduced { get; set; }
    public DateTime ProductionDate { get; set; }
    public string? Observation { get; set; }
    public Guid PlantId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Información del producto (opcional, según include)
    /// </summary>
    public ProductDto? Product { get; set; }
    
    /// <summary>
    /// Información de la planta (opcional, según include)
    /// </summary>
    public PlantProductionDto? Plant { get; set; }
}

/// <summary>
/// DTO de respuesta detallada para creación de producción
/// Incluye información sobre movimientos de almacén e inventario
/// </summary>
public class ProductionCreatedResponseDto
{
    public Guid Id { get; set; }
    public string Producto { get; set; } = string.Empty;
    public int CantidadProducida { get; set; }
    public DateTime FechaProduccion { get; set; }
    public string? Observacion { get; set; }
    public string Planta { get; set; } = string.Empty;
    public Guid WarehouseIdMovimiento { get; set; }
    public WarehouseProductUpdatedDto? WarehouseProductUpdated { get; set; }
}

/// <summary>
/// DTO para información de inventario actualizado
/// </summary>
public class WarehouseProductUpdatedDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public DateTime EntryDate { get; set; }
}
