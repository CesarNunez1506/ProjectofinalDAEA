using Application.DTOs.Production;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para obtener una producción por ID
/// </summary>
public class GetProductionByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductionByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductionDto?> ExecuteAsync(Guid id)
    {
        var productionRepo = _unitOfWork.GetRepository<Domain.Entities.Production>();
        
        // Obtener producción con Include de Product
        var productions = await productionRepo.GetAsync(
            filter: p => p.Id == id,
            includeProperties: "Product"
        );
        var production = productions.FirstOrDefault();
        
        if (production == null)
            return null;

        return new ProductionDto
        {
            Id = production.Id,
            ProductId = production.ProductId,
            QuantityProduced = production.QuantityProduced,
            ProductionDate = production.ProductionDate,
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
