namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando no hay suficiente stock disponible
/// </summary>
public class InsufficientStockException : Exception
{
    public Guid ProductId { get; }
    public Guid WarehouseId { get; }
    public int RequiredQuantity { get; }
    public int AvailableQuantity { get; }

    public InsufficientStockException(
        Guid productId,
        Guid warehouseId,
        int requiredQuantity,
        int availableQuantity)
        : base($"Stock insuficiente. Requerido: {requiredQuantity}, Disponible: {availableQuantity}")
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        RequiredQuantity = requiredQuantity;
        AvailableQuantity = availableQuantity;
    }
}
