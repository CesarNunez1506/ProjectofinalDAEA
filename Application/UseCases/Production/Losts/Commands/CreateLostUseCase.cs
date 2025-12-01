using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para crear una nueva pérdida
/// </summary>
public class CreateLostUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLostUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LostDto> ExecuteAsync(CreateLostDto dto)
    {
        var productionRepo = _unitOfWork.GetRepository<Domain.Entities.Production>();
        var lostRepo = _unitOfWork.GetRepository<Lost>();
        
        // Validar que la producción existe
        var productionExists = await productionRepo.AnyAsync(p => p.Id == dto.ProductionId);
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
            Observations = dto.Observations ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await lostRepo.AddAsync(lost);
        await _unitOfWork.SaveChangesAsync();

        return new LostDto
        {
            Id = lost.Id,
            ProductionId = lost.ProductionId,
            Quantity = lost.Quantity,
            LostType = lost.LostType,
            Observations = lost.Observations,
            CreatedAt = lost.CreatedAt,
            UpdatedAt = lost.UpdatedAt
        };
    }
}
