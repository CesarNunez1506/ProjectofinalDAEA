using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para eliminar (hard delete) una receta
/// </summary>
public class DeleteRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;

    public DeleteRecipeUseCase(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
        {
            throw new KeyNotFoundException($"No se encontró la receta con ID {id}");
        }

        // Hard delete - elimina permanentemente
        return await _recipeRepository.DeleteAsync(id);
    }
}
