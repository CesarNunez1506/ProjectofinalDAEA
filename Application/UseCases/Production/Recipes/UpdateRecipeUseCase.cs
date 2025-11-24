using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para actualizar una receta existente
/// Solo permite actualizar cantidad y unidad
/// </summary>
public class UpdateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;

    public UpdateRecipeUseCase(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<RecipeDto> ExecuteAsync(Guid id, UpdateRecipeDto dto)
    {
        var recipe = await _recipeRepository.GetByIdWithRelationsAsync(id);
        if (recipe == null)
        {
            throw new KeyNotFoundException($"No se encontró la receta con ID {id}");
        }

        // Solo actualizar cantidad y unidad (no se permite cambiar producto o recurso)
        if (dto.Quantity.HasValue)
        {
            recipe.Quantity = dto.Quantity.Value;
        }

        if (!string.IsNullOrEmpty(dto.Unit))
        {
            recipe.Unit = dto.Unit;
        }

        recipe.UpdatedAt = DateTime.UtcNow;

        var updated = await _recipeRepository.UpdateAsync(recipe);

        return new RecipeDto
        {
            Id = updated.Id,
            ProductId = updated.ProductId,
            ResourceId = updated.ResourceId,
            Quantity = updated.Quantity,
            Unit = updated.Unit,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
