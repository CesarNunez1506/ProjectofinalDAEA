namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para órdenes de compra
/// </summary>
public class BuysProductDto
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = null!;
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = null!;
    public DateTime PurchaseDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// DTO para crear una orden de compra
/// </summary>
public class CreateBuysProductDto
{
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<PurchaseItemDto> Items { get; set; } = new();
}

/// <summary>
/// DTO para items de una compra
/// </summary>
public class PurchaseItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
