namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando se intenta crear un proveedor con RUC duplicado
/// </summary>
public class DuplicateSupplierException : Exception
{
    public long Ruc { get; }

    public DuplicateSupplierException(long ruc)
        : base($"Ya existe un proveedor con el RUC: {ruc}")
    {
        Ruc = ruc;
    }
}
