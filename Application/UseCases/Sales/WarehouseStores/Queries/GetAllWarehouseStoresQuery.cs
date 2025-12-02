using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.WarehouseStores.Queries;

public class GetAllWarehouseStoresQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllWarehouseStoresQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<WarehouseStoreDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<WarehouseStore>();
        var warehouseStores = await repo.GetAllAsync();

        return warehouseStores.Select(ws => new WarehouseStoreDto
        {
            Id = ws.Id,
            StoreId = ws.StoreId,
            ProductId = ws.ProductId,
            Quantity = ws.Quantity,
            CreatedAt = ws.CreatedAt,
            UpdatedAt = ws.UpdatedAt
        });
    }
}
