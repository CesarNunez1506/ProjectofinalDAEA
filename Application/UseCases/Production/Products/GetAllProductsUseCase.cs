using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para obtener todos los productos con sus categorías
/// </summary>
public class GetAllProductsUseCase
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> ExecuteAsync()
    {
        var products = await _productRepository.GetAllWithCategoryAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            Price = p.Price,
            Description = p.Description,
            ImagenUrl = p.ImagenUrl,
            Status = p.Status,
            Producible = p.Producible,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            Category = p.Category != null ? new CategoryDto
            {
                Id = p.Category.Id,
                Name = p.Category.Name,
                Description = p.Category.Description,
                Status = p.Category.Status
            } : null
        });
    }
}
