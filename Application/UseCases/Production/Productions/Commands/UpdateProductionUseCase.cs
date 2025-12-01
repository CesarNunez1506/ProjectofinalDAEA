using Application.DTOs.Production;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para actualizar una producción existente
/// Solo permite actualizar cantidad y fecha
/// </summary>
public class UpdateProductionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductionUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductionDto> ExecuteAsync(Guid id, UpdateProductionDto dto)
    {
        var productionRepo = _unitOfWork.GetRepository<Domain.Entities.Production>();
        
        var production = await productionRepo.GetByIdAsync(id);
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

        productionRepo.Update(production);
        await _unitOfWork.SaveChangesAsync();

        return new ProductionDto
        {
            Id = production.Id,
            ProductId = production.ProductId,
            QuantityProduced = production.QuantityProduced,
            ProductionDate = production.ProductionDate,
            IsActive = production.IsActive,
            CreatedAt = production.CreatedAt,
            UpdatedAt = production.UpdatedAt
        };
    }
}
