using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para actualizar una pérdida existente
/// </summary>
public class UpdateLostUseCase
{
    private readonly ILostRepository _lostRepository;

    public UpdateLostUseCase(ILostRepository lostRepository)
    {
        _lostRepository = lostRepository;
    }

    public async Task<LostDto> ExecuteAsync(Guid id, UpdateLostDto dto)
    {
        var lost = await _lostRepository.GetByIdAsync(id);
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

        var updated = await _lostRepository.UpdateAsync(lost);

        return new LostDto
        {
            Id = updated.Id,
            ProductionId = updated.ProductionId,
            Quantity = updated.Quantity,
            LostType = updated.LostType,
            Observations = updated.Observations,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
