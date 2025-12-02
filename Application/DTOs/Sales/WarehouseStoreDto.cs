namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear un registro de inventario en tienda
/// </summary>
public class CreateWarehouseStoreDto
{
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }
}

/// <summary>
/// DTO para actualizar inventario en tienda
/// </summary>
public class UpdateWarehouseStoreDto
{
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }
}

/// <summary>
/// DTO de respuesta para inventario en tienda
/// </summary>
public class WarehouseStoreDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
