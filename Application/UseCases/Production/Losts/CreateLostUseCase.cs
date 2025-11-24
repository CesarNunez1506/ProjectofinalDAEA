using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para crear una nueva pérdida
/// </summary>
public class CreateLostUseCase
{
    private readonly ILostRepository _lostRepository;
    private readonly IProductionRepository _productionRepository;

    public CreateLostUseCase(
        ILostRepository lostRepository,
        IProductionRepository productionRepository)
    {
        _lostRepository = lostRepository;
        _productionRepository = productionRepository;
    }

    public async Task<LostDto> ExecuteAsync(CreateLostDto dto)
    {
        // Validar que la producción existe
        var productionExists = await _productionRepository.ExistsAsync(dto.ProductionId);
        if (!productionExists)
        {
            throw new KeyNotFoundException($"La producción con ID {dto.ProductionId} no existe");
        }

        var lost = new Lost
        {
            Id = Guid.NewGuid(),
            ProductionId = dto.ProductionId,
            Quantity = dto.Quantity,
            LostType = dto.LostType,
            Description = dto.Description ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _lostRepository.CreateAsync(lost);

        return new LostDto
        {
            Id = created.Id,
            ProductionId = created.ProductionId,
            Quantity = created.Quantity,
            LostType = created.LostType,
            Description = created.Description,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
