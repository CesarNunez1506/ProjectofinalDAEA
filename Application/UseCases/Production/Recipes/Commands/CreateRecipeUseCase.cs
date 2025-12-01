using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para crear una nueva receta
/// </summary>
public class CreateRecipeUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRecipeUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RecipeDto> ExecuteAsync(CreateRecipeDto dto)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        var resourceRepo = _unitOfWork.GetRepository<PlantProduction>();
        var recipeRepo = _unitOfWork.GetRepository<Recipe>();
        
        // Validar que el producto existe
        var productExists = await productRepo.AnyAsync(p => p.Id == dto.ProductId);
        if (!productExists)
        {
            throw new KeyNotFoundException($"El producto con ID {dto.ProductId} no existe");
        }

        // Validar que el recurso (PlantProduction) existe
        var resourceExists = await resourceRepo.AnyAsync(r => r.Id == dto.ResourceId);
        if (!resourceExists)
        {
            throw new KeyNotFoundException($"El recurso con ID {dto.ResourceId} no existe");
        }

        // Verificar que no exista una receta duplicada para este producto y recurso
        var exists = await recipeRepo.AnyAsync(r => r.ProductId == dto.ProductId && r.ResourceId == dto.ResourceId);
        if (exists)
        {
            throw new InvalidOperationException($"Ya existe una receta para este producto y recurso");
        }

        var recipe = new Recipe
        {
            Id = Guid.NewGuid(),
            ProductId = dto.ProductId,
            ResourceId = dto.ResourceId,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await recipeRepo.AddAsync(recipe);
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
