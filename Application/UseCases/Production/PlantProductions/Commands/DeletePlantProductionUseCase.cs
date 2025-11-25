using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para eliminar (soft delete) una producción de planta
/// </summary>
public class DeletePlantProductionUseCase
{
    private readonly IPlantProductionRepository _plantProductionRepository;

    public DeletePlantProductionUseCase(IPlantProductionRepository plantProductionRepository)
    {
        _plantProductionRepository = plantProductionRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var plantProduction = await _plantProductionRepository.GetByIdAsync(id);
        if (plantProduction == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción de planta con ID {id}");
        }

        // Soft delete - marca como inactivo
        return await _plantProductionRepository.SoftDeleteAsync(id);
    }
}
