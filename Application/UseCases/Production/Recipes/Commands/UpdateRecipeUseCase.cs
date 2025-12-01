using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para actualizar una receta existente
/// Solo permite actualizar cantidad y unidad
/// </summary>
public class UpdateRecipeUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRecipeUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RecipeDto> ExecuteAsync(Guid id, UpdateRecipeDto dto)
    {
        var recipeRepo = _unitOfWork.GetRepository<Recipe>();
        
        // Obtener receta con relaciones
        var recipes = await recipeRepo.GetAsync(
            filter: r => r.Id == id,
            includeProperties: "Product,Resource"
        );
        var recipe = recipes.FirstOrDefault();
        
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

        recipeRepo.Update(recipe);
        await _unitOfWork.SaveChangesAsync();

        return new RecipeDto
        {
            Id = recipe.Id,
            ProductId = recipe.ProductId,
            ResourceId = recipe.ResourceId,
            Quantity = recipe.Quantity,
            Unit = recipe.Unit,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };
    }
}
