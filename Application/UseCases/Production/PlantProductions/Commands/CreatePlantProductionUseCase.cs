using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para crear una nueva planta de producción
/// </summary>
public class CreatePlantProductionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlantProductionUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PlantProductionDto> ExecuteAsync(CreatePlantProductionDto dto)
    {
        var plantRepo = _unitOfWork.GetRepository<PlantProduction>();
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();

        // Verificar si ya existe una planta con el mismo nombre
        var existingPlant = await plantRepo.FirstOrDefaultAsync(p => p.PlantName == dto.PlantName);
        if (existingPlant != null)
        {
            throw new InvalidOperationException($"Ya existe una planta con el nombre '{dto.PlantName}'");
        }

        // Verificar que el almacén existe
        var warehouseExists = await warehouseRepo.AnyAsync(w => w.Id == dto.WarehouseId);
        if (!warehouseExists)
        {
            throw new KeyNotFoundException($"El almacén con ID {dto.WarehouseId} no existe");
        }

        var plant = new PlantProduction
        {
            Id = Guid.NewGuid(),
            PlantName = dto.PlantName,
            Address = dto.Address,
            WarehouseId = dto.WarehouseId,
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await plantRepo.AddAsync(plant);
        await _unitOfWork.SaveChangesAsync();

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
