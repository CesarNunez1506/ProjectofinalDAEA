namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando no se encuentra un producto
/// </summary>
public class ProductNotFoundException : Exception
{
    public Guid ProductId { get; }

    public ProductNotFoundException(Guid productId)
        : base($"No se encontró el producto con ID: {productId}")
    {
        ProductId = productId;
    }

    public ProductNotFoundException(Guid productId, string message)
        : base(message)
    {
        ProductId = productId;
    }
}
