using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para obtener todas las producciones de planta
/// </summary>
public class GetAllPlantProductionsUseCase
{
    private readonly IPlantProductionRepository _plantProductionRepository;

    public GetAllPlantProductionsUseCase(IPlantProductionRepository plantProductionRepository)
    {
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<IEnumerable<PlantProductionDto>> ExecuteAsync()
    {
        var plantProductions = await _plantProductionRepository.GetAllAsync();

        return plantProductions.Select(p => new PlantProductionDto
        {
            Id = p.Id,
            Name = p.Name,
            Unit = p.Unit,
            Quantity = p.Quantity,
            Status = p.Status,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }
}
