namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para transferir datos de Warehouse
/// </summary>
public class WarehouseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string? Observation { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CurrentLoad { get; set; }
}

/// <summary>
/// DTO para crear un nuevo almacén
/// </summary>
public class CreateWarehouseDto
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string? Observation { get; set; }
}

/// <summary>
/// DTO para actualizar un almacén
/// </summary>
public class UpdateWarehouseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string? Observation { get; set; }
    public bool Status { get; set; }
}
