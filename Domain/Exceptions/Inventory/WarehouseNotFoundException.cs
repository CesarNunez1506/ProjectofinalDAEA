namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando no se encuentra un almacén
/// </summary>
public class WarehouseNotFoundException : Exception
{
    public Guid WarehouseId { get; }

    public WarehouseNotFoundException(Guid warehouseId)
        : base($"No se encontró el almacén con ID: {warehouseId}")
    {
        WarehouseId = warehouseId;
    }
}
