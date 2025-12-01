using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para obtener todas las plantas de producción
/// </summary>
public class GetAllPlantProductionsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPlantProductionsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlantProductionDto>> ExecuteAsync()
    {
        var plantRepo = _unitOfWork.GetRepository<PlantProduction>();

        // Obtener todas las plantas
        var plants = await plantRepo.GetAllAsync();

        return plants.Select(p => new PlantProductionDto
        {
            Id = p.Id,
            PlantName = p.PlantName,
            Address = p.Address,
            WarehouseId = p.WarehouseId,
            Status = p.Status,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }
}
