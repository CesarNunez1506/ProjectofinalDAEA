using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para eliminar (soft delete) una producción de planta
/// </summary>
public class DeletePlantProductionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePlantProductionUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var plantRepo = _unitOfWork.GetRepository<PlantProduction>();
        
        var plantProduction = await plantRepo.GetByIdAsync(id);
        if (plantProduction == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción de planta con ID {id}");
        }

        // Soft delete - marca como inactivo
        plantProduction.Status = false;
        plantProduction.UpdatedAt = DateTime.UtcNow;
        plantRepo.Update(plantProduction);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
