using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Stores.Queries;

/// <summary>
/// Caso de uso para obtener una tienda por ID
/// </summary>
public class GetStoreByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStoreByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StoreDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Store>();
        var store = await repo.GetByIdAsync(id);

        if (store == null)
            return null;

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
