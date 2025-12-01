using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para obtener todos los productos con sus categorías
/// </summary>
public class GetAllProductsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDto>> ExecuteAsync()
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        
        // Obtener todos los productos con Include de Category
        var products = await productRepo.GetAsync(
            includeProperties: "Category"
        );

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
