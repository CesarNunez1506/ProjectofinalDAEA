using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para actualizar una producción de planta existente
/// </summary>
public class UpdatePlantProductionUseCase
{
    private readonly IPlantProductionRepository _plantProductionRepository;

    public UpdatePlantProductionUseCase(IPlantProductionRepository plantProductionRepository)
    {
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<PlantProductionDto> ExecuteAsync(Guid id, UpdatePlantProductionDto dto)
    {
        var plantProduction = await _plantProductionRepository.GetByIdAsync(id);
        if (plantProduction == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción de planta con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Name))
        {
            plantProduction.Name = dto.Name;
        }

        if (!string.IsNullOrEmpty(dto.Unit))
        {
            plantProduction.Unit = dto.Unit;
        }

        if (dto.Quantity.HasValue)
        {
            plantProduction.Quantity = dto.Quantity.Value;
        }

        if (dto.Status.HasValue)
        {
            plantProduction.Status = dto.Status.Value;
        }

        plantProduction.UpdatedAt = DateTime.UtcNow;

        var updated = await _plantProductionRepository.UpdateAsync(plantProduction);

        return new PlantProductionDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Unit = updated.Unit,
            Quantity = updated.Quantity,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
