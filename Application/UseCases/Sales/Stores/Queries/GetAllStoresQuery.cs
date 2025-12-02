using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Stores.Queries;

/// <summary>
/// Caso de uso para obtener todas las tiendas
/// </summary>
public class GetAllStoresQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllStoresQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<StoreDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<Store>();
        var stores = await repo.GetAllAsync();

        return stores.Select(s => new StoreDto
        {
            Id = s.Id,
            StoreName = s.StoreName,
            Address = s.Address,
            Observations = s.Observations,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        });
    }
}
