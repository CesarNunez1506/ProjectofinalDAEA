namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para stock de productos en almacenes
/// </summary>
public class WarehouseProductDto
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = null!;
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime EntryDate { get; set; }
    public bool? Status { get; set; }
}

/// <summary>
/// DTO para agregar stock de producto a un almacén
/// </summary>
public class AddWarehouseProductDto
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

/// <summary>
/// DTO para ajustar stock
/// </summary>
public class AdjustStockDto
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = null!;
}
