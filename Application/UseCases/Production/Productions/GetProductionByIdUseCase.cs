using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para obtener una producción por ID
/// </summary>
public class GetProductionByIdUseCase
{
    private readonly IProductionRepository _productionRepository;

    public GetProductionByIdUseCase(IProductionRepository productionRepository)
    {
        _productionRepository = productionRepository;
    }

    public async Task<ProductionDto?> ExecuteAsync(Guid id)
    {
        var production = await _productionRepository.GetByIdAsync(id);
        
        if (production == null)
            return null;

        return new ProductionDto
        {
            Id = production.Id,
            ProductId = production.ProductId,
            Quantity = production.Quantity,
            Date = production.Date,
            IsActive = production.IsActive,
            CreatedAt = production.CreatedAt,
            UpdatedAt = production.UpdatedAt,
            Product = production.Product != null ? new ProductDto
            {
                Id = production.Product.Id,
                Name = production.Product.Name,
                CategoryId = production.Product.CategoryId,
                Price = production.Product.Price,
                Description = production.Product.Description,
                ImagenUrl = production.Product.ImagenUrl,
                Status = production.Product.Status,
                Producible = production.Product.Producible
            } : null
        };
    }
}
