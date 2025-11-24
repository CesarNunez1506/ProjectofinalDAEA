using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para obtener una producción de planta por ID
/// </summary>
public class GetPlantProductionByIdUseCase
{
    private readonly IPlantProductionRepository _plantProductionRepository;

    public GetPlantProductionByIdUseCase(IPlantProductionRepository plantProductionRepository)
    {
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<PlantProductionDto?> ExecuteAsync(Guid id)
    {
        var plantProduction = await _plantProductionRepository.GetByIdAsync(id);
        
        if (plantProduction == null)
            return null;

        return new PlantProductionDto
        {
            Id = plantProduction.Id,
            Name = plantProduction.Name,
            Unit = plantProduction.Unit,
            Quantity = plantProduction.Quantity,
            Status = plantProduction.Status,
            CreatedAt = plantProduction.CreatedAt,
            UpdatedAt = plantProduction.UpdatedAt
        };
    }
}
