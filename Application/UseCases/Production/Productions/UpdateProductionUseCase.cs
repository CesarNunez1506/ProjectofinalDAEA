using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para actualizar una producción existente
/// Solo permite actualizar cantidad y fecha
/// </summary>
public class UpdateProductionUseCase
{
    private readonly IProductionRepository _productionRepository;

    public UpdateProductionUseCase(IProductionRepository productionRepository)
    {
        _productionRepository = productionRepository;
    }

    public async Task<ProductionDto> ExecuteAsync(Guid id, UpdateProductionDto dto)
    {
        var production = await _productionRepository.GetByIdAsync(id);
        if (production == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción con ID {id}");
        }

        // Actualizar solo los campos permitidos
        if (dto.QuantityProduced.HasValue)
        {
            production.QuantityProduced = dto.QuantityProduced.Value;
        }

        if (dto.ProductionDate.HasValue)
        {
            production.ProductionDate = dto.ProductionDate.Value;
        }

        production.UpdatedAt = DateTime.UtcNow;

        var updated = await _productionRepository.UpdateAsync(production);

        return new ProductionDto
        {
            Id = updated.Id,
            ProductId = updated.ProductId,
            QuantityProduced = updated.QuantityProduced,
            ProductionDate = updated.ProductionDate,
            IsActive = updated.IsActive,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
