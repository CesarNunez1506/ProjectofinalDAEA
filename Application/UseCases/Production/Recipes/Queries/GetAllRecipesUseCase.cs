using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Recipes;

/// <summary>
/// Caso de uso para obtener todas las recetas con sus relaciones
/// </summary>
public class GetAllRecipesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRecipesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RecipeDto>> ExecuteAsync()
    {
        var recipeRepo = _unitOfWork.GetRepository<Recipe>();
        
        // Obtener todas las recetas con relaciones
        var recipes = await recipeRepo.GetAsync(
            includeProperties: "Product,Resource"
        );

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
