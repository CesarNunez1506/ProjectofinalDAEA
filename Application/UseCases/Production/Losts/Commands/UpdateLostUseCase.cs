using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para actualizar una pérdida existente
/// </summary>
public class UpdateLostUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLostUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LostDto> ExecuteAsync(Guid id, UpdateLostDto dto)
    {
        var lostRepo = _unitOfWork.GetRepository<Lost>();
        
        var lost = await lostRepo.GetByIdAsync(id);
        if (lost == null)
        {
            throw new KeyNotFoundException($"No se encontró la pérdida con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (dto.Quantity.HasValue)
        {
            lost.Quantity = dto.Quantity.Value;
        }

        if (!string.IsNullOrEmpty(dto.LostType))
        {
            lost.LostType = dto.LostType;
        }

        if (dto.Observations != null)
        {
            lost.Observations = dto.Observations;
        }

        lost.UpdatedAt = DateTime.UtcNow;

        lostRepo.Update(lost);
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
