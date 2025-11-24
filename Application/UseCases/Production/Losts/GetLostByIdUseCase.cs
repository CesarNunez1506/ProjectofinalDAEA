using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para obtener una pérdida por ID
/// </summary>
public class GetLostByIdUseCase
{
    private readonly ILostRepository _lostRepository;

    public GetLostByIdUseCase(ILostRepository lostRepository)
    {
        _lostRepository = lostRepository;
    }

    public async Task<LostDto?> ExecuteAsync(Guid id)
    {
        var lost = await _lostRepository.GetByIdAsync(id);
        
        if (lost == null)
            return null;

        return new LostDto
        {
            Id = lost.Id,
            ProductionId = lost.ProductionId,
            Quantity = lost.Quantity,
            LostType = lost.LostType,
            Description = lost.Description,
            CreatedAt = lost.CreatedAt,
            UpdatedAt = lost.UpdatedAt,
            Production = lost.Production != null ? new ProductionDto
            {
                Id = lost.Production.Id,
                ProductId = lost.Production.ProductId,
                Quantity = lost.Production.Quantity,
                Date = lost.Production.Date,
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
