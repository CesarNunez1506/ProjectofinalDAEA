using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para obtener un producto por ID con su categoría
/// </summary>
public class GetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> ExecuteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id);
        
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Description = product.Description,
            ImagenUrl = product.ImagenUrl,
            Status = product.Status,
            Producible = product.Producible,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Category = product.Category != null ? new CategoryDto
            {
                Id = product.Category.Id,
                Name = product.Category.Name,
                Description = product.Category.Description,
                Status = product.Category.Status
            } : null
        };
    }
}
