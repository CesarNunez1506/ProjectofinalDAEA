using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Stores.Commands;

/// <summary>
/// Caso de uso para actualizar una tienda existente
/// </summary>
public class UpdateStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StoreDto> ExecuteAsync(Guid id, UpdateStoreDto dto)
    {
        var repo = _unitOfWork.GetRepository<Store>();

        var store = await repo.GetByIdAsync(id);
        if (store == null)
            throw new KeyNotFoundException($"Tienda con ID {id} no encontrada");

        // Validar nombre único (excluyendo la tienda actual)
        var exists = await repo.AnyAsync(s => s.StoreName == dto.StoreName && s.Id != id);
        if (exists)
            throw new InvalidOperationException($"Ya existe otra tienda con el nombre '{dto.StoreName}'");

        store.StoreName = dto.StoreName;
        store.Address = dto.Address;
        store.Observations = dto.Observations;
        store.UpdatedAt = DateTime.UtcNow;

        repo.Update(store);
        await _unitOfWork.SaveChangesAsync();

        return new StoreDto
        {
            Id = store.Id,
            StoreName = store.StoreName,
            Address = store.Address,
            Observations = store.Observations,
            CreatedAt = store.CreatedAt,
            UpdatedAt = store.UpdatedAt
        };
    }
}
