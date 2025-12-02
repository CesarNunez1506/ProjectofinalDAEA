namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando se excede la capacidad de un almacén
/// </summary>
public class WarehouseCapacityExceededException : Exception
{
    public Guid WarehouseId { get; }
    public int Capacity { get; }
    public int CurrentLoad { get; }

    public WarehouseCapacityExceededException(Guid warehouseId, int capacity, int currentLoad)
        : base($"El almacén ha excedido su capacidad. Capacidad: {capacity}, Carga actual: {currentLoad}")
    {
        WarehouseId = warehouseId;
        Capacity = capacity;
        CurrentLoad = currentLoad;
    }
}
