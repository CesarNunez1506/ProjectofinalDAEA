using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para obtener una pérdida por ID
/// </summary>
public class GetLostByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLostByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LostDto?> ExecuteAsync(Guid id)
    {
        var lostRepo = _unitOfWork.GetRepository<Lost>();
        
        // Obtener pérdida con Include de Production y Product
        var losts = await lostRepo.GetAsync(
            filter: l => l.Id == id,
            includeProperties: "Production,Production.Product"
        );
        var lost = losts.FirstOrDefault();
        
        if (lost == null)
            return null;

        return new LostDto
        {
            Id = lost.Id,
            ProductionId = lost.ProductionId,
            Quantity = lost.Quantity,
            LostType = lost.LostType,
            Observations = lost.Observations,
            CreatedAt = lost.CreatedAt,
            UpdatedAt = lost.UpdatedAt,
            Production = lost.Production != null ? new ProductionDto
            {
                Id = lost.Production.Id,
                ProductId = lost.Production.ProductId,
                QuantityProduced = lost.Production.QuantityProduced,
                ProductionDate = lost.Production.ProductionDate,
                IsActive = lost.Production.IsActive,
                Product = lost.Production.Product != null ? new ProductDto
                {
                    Id = lost.Production.Product.Id,
                    Name = lost.Production.Product.Name,
                    CategoryId = lost.Production.Product.CategoryId,
                    Price = lost.Production.Product.Price
                } : null
            } : null
        };
    }
}
