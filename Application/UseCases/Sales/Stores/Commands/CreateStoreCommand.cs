using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Stores.Commands;

/// <summary>
/// Caso de uso para crear una nueva tienda
/// </summary>
public class CreateStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StoreDto> ExecuteAsync(CreateStoreDto dto)
    {
        var repo = _unitOfWork.GetRepository<Store>();

        // Validar nombre único
        var exists = await repo.AnyAsync(s => s.StoreName == dto.StoreName);
        if (exists)
            throw new InvalidOperationException($"Ya existe una tienda con el nombre '{dto.StoreName}'");

        var store = new Store
        {
            Id = Guid.NewGuid(),
            StoreName = dto.StoreName,
            Address = dto.Address,
            Observations = dto.Observations,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(store);
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
