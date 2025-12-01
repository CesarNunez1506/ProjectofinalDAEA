using Application.DTOs.Production;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para obtener todas las producciones
/// </summary>
public class GetAllProductionsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductionsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductionDto>> ExecuteAsync()
    {
        var productionRepo = _unitOfWork.GetRepository<Domain.Entities.Production>();
        
        // Obtener todas las producciones con Include de Product
        var productions = await productionRepo.GetAsync(
            includeProperties: "Product"
        );

        return productions.Select(p => new ProductionDto
        {
            Id = p.Id,
            ProductId = p.ProductId,
            QuantityProduced = p.QuantityProduced,
            ProductionDate = p.ProductionDate,
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
