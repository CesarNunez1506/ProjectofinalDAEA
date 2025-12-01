using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para obtener todas las pérdidas con sus relaciones
/// </summary>
public class GetAllLostsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLostsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<LostDto>> ExecuteAsync()
    {
        var lostRepo = _unitOfWork.GetRepository<Lost>();
        
        // Obtener todas las pérdidas con Include de Production y Product
        var losts = await lostRepo.GetAsync(
            includeProperties: "Production,Production.Product"
        );

        return losts.Select(l => new LostDto
        {
            Id = l.Id,
            ProductionId = l.ProductionId,
            Quantity = l.Quantity,
            LostType = l.LostType,
            Observations = l.Observations,
            CreatedAt = l.CreatedAt,
            UpdatedAt = l.UpdatedAt,
            Production = l.Production != null ? new ProductionDto
            {
                Id = l.Production.Id,
                ProductId = l.Production.ProductId,
                QuantityProduced = l.Production.QuantityProduced,
                ProductionDate = l.Production.ProductionDate,
                IsActive = l.Production.IsActive,
                Product = l.Production.Product != null ? new ProductDto
                {
                    Id = l.Production.Product.Id,
                    Name = l.Production.Product.Name,
                    CategoryId = l.Production.Product.CategoryId,
                    Price = l.Production.Product.Price
                } : null
            } : null
        });
    }
}
