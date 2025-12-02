namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para transferir datos de Resource
/// </summary>
public class ResourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Observation { get; set; }
    public bool Status { get; set; }
    public Guid? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// DTO para crear un nuevo recurso
/// </summary>
public class CreateResourceDto
{
    public string Name { get; set; } = null!;
    public string? Observation { get; set; }
    public Guid? SupplierId { get; set; }
}

/// <summary>
/// DTO para actualizar un recurso
/// </summary>
public class UpdateResourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Observation { get; set; }
    public bool Status { get; set; }
    public Guid? SupplierId { get; set; }
}
