using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para eliminar (soft delete) una categoría
/// </summary>
public class DeleteCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");
        }

        // Verificar si tiene productos activos asociados
        var hasActiveProducts = await _categoryRepository.HasActiveProductsAsync(id);
        if (hasActiveProducts)
        {
            throw new InvalidOperationException(
                "No se puede eliminar la categoría porque tiene productos activos asociados");
        }

        return await _categoryRepository.SoftDeleteAsync(id);
    }
}
