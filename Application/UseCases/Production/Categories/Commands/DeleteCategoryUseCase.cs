using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para eliminar (soft delete) una categoría
/// </summary>
public class DeleteCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var productRepo = _unitOfWork.GetRepository<Product>();
        
        var category = await categoryRepo.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");
        }

        // Verificar si tiene productos activos asociados
        var hasActiveProducts = await productRepo.AnyAsync(p => p.CategoryId == id && p.Status);
        if (hasActiveProducts)
        {
            throw new InvalidOperationException(
                "No se puede eliminar la categoría porque tiene productos activos asociados");
        }

        category.Status = false;
        category.UpdatedAt = DateTime.UtcNow;
        categoryRepo.Update(category);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
