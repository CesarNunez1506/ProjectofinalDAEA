namespace Domain.Exceptions.Inventory;

/// <summary>
/// Excepción lanzada cuando no se encuentra una categoría
/// </summary>
public class CategoryNotFoundException : Exception
{
    public Guid CategoryId { get; }

    public CategoryNotFoundException(Guid categoryId)
        : base($"No se encontró la categoría con ID: {categoryId}")
    {
        CategoryId = categoryId;
    }
}
