using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para eliminar (hard delete) una receta
/// </summary>
public class DeleteRecipeUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRecipeUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var recipeRepo = _unitOfWork.GetRepository<Recipe>();
        
        var recipe = await recipeRepo.GetByIdAsync(id);
        if (recipe == null)
        {
            throw new KeyNotFoundException($"No se encontró la receta con ID {id}");
        }

        // Hard delete - elimina permanentemente
        recipeRepo.Remove(recipe);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
