using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para obtener todas las recetas de un producto específico
/// </summary>
public class GetRecipesByProductIdUseCase
{
    private readonly IRecipeRepository _recipeRepository;

    public GetRecipesByProductIdUseCase(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<IEnumerable<RecipeDto>> ExecuteAsync(Guid productId)
    {
        var recipes = await _recipeRepository.GetByProductIdAsync(productId);

        return recipes.Select(r => new RecipeDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            ResourceId = r.ResourceId,
            Quantity = r.Quantity,
            Unit = r.Unit,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            Product = r.Product != null ? new ProductDto
            {
                Id = r.Product.Id,
                Name = r.Product.Name,
                CategoryId = r.Product.CategoryId,
                Price = r.Product.Price,
                Description = r.Product.Description,
                ImagenUrl = r.Product.ImagenUrl,
                Status = r.Product.Status,
                Producible = r.Product.Producible
            } : null,
            Resource = r.Resource != null ? new ResourceDto
            {
                Id = r.Resource.Id,
                Name = r.Resource.Name,
                Observation = r.Resource.Observation,
                Status = r.Resource.Status
            } : null
        });
    }
}
