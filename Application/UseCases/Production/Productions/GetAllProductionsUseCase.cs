using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para obtener todas las producciones
/// </summary>
public class GetAllProductionsUseCase
{
    private readonly IProductionRepository _productionRepository;

    public GetAllProductionsUseCase(IProductionRepository productionRepository)
    {
        _productionRepository = productionRepository;
    }

    public async Task<IEnumerable<ProductionDto>> ExecuteAsync()
    {
        var productions = await _productionRepository.GetAllAsync();

        return productions.Select(p => new ProductionDto
        {
            Id = p.Id,
            ProductId = p.ProductId,
            Quantity = p.Quantity,
            Date = p.Date,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            Product = p.Product != null ? new ProductDto
            {
                Id = p.Product.Id,
                Name = p.Product.Name,
                CategoryId = p.Product.CategoryId,
                Price = p.Product.Price,
                Description = p.Product.Description,
                ImagenUrl = p.Product.ImagenUrl,
                Status = p.Product.Status,
                Producible = p.Product.Producible
            } : null
        });
    }
}
