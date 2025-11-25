using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.PlantProductions;

/// <summary>
/// Caso de uso para actualizar una planta de producción existente
/// </summary>
public class UpdatePlantProductionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePlantProductionUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PlantProductionDto> ExecuteAsync(Guid id, UpdatePlantProductionDto dto)
    {
        var plantRepo = _unitOfWork.GetRepository<PlantProduction>();
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();

        var plant = await plantRepo.GetByIdAsync(id);
        if (plant == null)
        {
            throw new KeyNotFoundException($"No se encontró la planta de producción con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.PlantName))
        {
            // Verificar si el nuevo nombre ya existe en otra planta
            var existingPlant = await plantRepo.FirstOrDefaultAsync(p => p.PlantName == dto.PlantName);
            if (existingPlant != null && existingPlant.Id != id)
            {
                throw new InvalidOperationException($"Ya existe otra planta con el nombre '{dto.PlantName}'");
            }
            plant.PlantName = dto.PlantName;
        }

        if (!string.IsNullOrEmpty(dto.Address))
        {
            plant.Address = dto.Address;
        }

        if (dto.WarehouseId.HasValue)
        {
            // Verificar que el nuevo almacén existe
            var warehouseExists = await warehouseRepo.AnyAsync(w => w.Id == dto.WarehouseId.Value);
            if (!warehouseExists)
            {
                throw new KeyNotFoundException($"El almacén con ID {dto.WarehouseId.Value} no existe");
            }
            plant.WarehouseId = dto.WarehouseId.Value;
        }

        if (dto.Status.HasValue)
        {
            plant.Status = dto.Status.Value;
        }

        plant.UpdatedAt = DateTime.UtcNow;

        plantRepo.Update(plant);
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
