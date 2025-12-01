using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para obtener un producto por ID con su categoría
/// </summary>
public class GetProductByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto?> ExecuteAsync(Guid id)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        
        // Obtener producto con Include de Category
        var products = await productRepo.GetAsync(
            filter: p => p.Id == id,
            includeProperties: "Category"
        );
        var product = products.FirstOrDefault();
        
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
