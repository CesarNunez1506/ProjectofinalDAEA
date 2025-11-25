namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando no se encuentra un proveedor
/// </summary>
public class SupplierNotFoundException : Exception
{
    public Guid SupplierId { get; }

    public SupplierNotFoundException(Guid supplierId)
        : base($"No se encontró el proveedor con ID: {supplierId}")
    {
        SupplierId = supplierId;
    }
}
