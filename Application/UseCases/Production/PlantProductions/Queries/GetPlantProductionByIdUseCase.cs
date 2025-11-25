using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para obtener una planta de producción por ID
/// </summary>
public class GetPlantProductionByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlantProductionByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PlantProductionDto?> ExecuteAsync(Guid id)
    {
        var plantRepo = _unitOfWork.GetRepository<PlantProduction>();

        // Obtener planta por ID
        var plant = await plantRepo.GetByIdAsync(id);

        if (plant == null)
            return null;

        return new PlantProductionDto
        {
            Id = plant.Id,
            PlantName = plant.PlantName,
            Address = plant.Address,
            WarehouseId = plant.WarehouseId,
            Status = plant.Status,
            CreatedAt = plant.CreatedAt,
            UpdatedAt = plant.UpdatedAt
        };
    }
}
