namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para movimientos de productos en almacenes
/// </summary>
public class WarehouseMovementProductDto
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = null!;
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string MovementType { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public string? Reason { get; set; }
}

/// <summary>
/// DTO para crear un movimiento de producto
/// </summary>
public class CreateWarehouseMovementDto
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public string MovementType { get; set; } = null!; // "entrada", "salida", "transferencia", "ajuste"
    public int Quantity { get; set; }
    public string? Reason { get; set; }
}

/// <summary>
/// DTO para transferir productos entre almacenes
/// </summary>
public class TransferProductDto
{
    public Guid SourceWarehouseId { get; set; }
    public Guid DestinationWarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
}
