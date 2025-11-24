using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para crear una nueva producción de planta
/// </summary>
public class CreatePlantProductionUseCase
{
    private readonly IPlantProductionRepository _plantProductionRepository;

    public CreatePlantProductionUseCase(IPlantProductionRepository plantProductionRepository)
    {
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<PlantProductionDto> ExecuteAsync(CreatePlantProductionDto dto)
    {
        var plantProduction = new PlantProduction
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Unit = dto.Unit,
            Quantity = dto.Quantity,
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _plantProductionRepository.CreateAsync(plantProduction);

        return new PlantProductionDto
        {
            Id = created.Id,
            Name = created.Name,
            Unit = created.Unit,
            Quantity = created.Quantity,
            Status = created.Status,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
