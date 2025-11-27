using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para crear una nueva receta
/// </summary>
public class CreateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPlantProductionRepository _plantProductionRepository;

    public CreateRecipeUseCase(
        IRecipeRepository recipeRepository,
        IProductRepository productRepository,
        IPlantProductionRepository plantProductionRepository)
    {
        _recipeRepository = recipeRepository;
        _productRepository = productRepository;
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<RecipeDto> ExecuteAsync(CreateRecipeDto dto)
    {
        // Validar que el producto existe
        var productExists = await _productRepository.ExistsAsync(dto.ProductId);
        if (!productExists)
        {
            throw new KeyNotFoundException($"El producto con ID {dto.ProductId} no existe");
        }

        // Validar que el recurso (PlantProduction) existe
        var resourceExists = await _plantProductionRepository.ExistsAsync(dto.ResourceId);
        if (!resourceExists)
        {
            throw new KeyNotFoundException($"El recurso con ID {dto.ResourceId} no existe");
        }

        // Verificar que no exista una receta duplicada para este producto y recurso
        var exists = await _recipeRepository.ExistsByProductAndResourceAsync(dto.ProductId, dto.ResourceId);
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

        var created = await _recipeRepository.CreateAsync(recipe);

        return new RecipeDto
        {
            Id = created.Id,
            ProductId = created.ProductId,
            ResourceId = created.ResourceId,
            Quantity = created.Quantity,
            Unit = created.Unit,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
