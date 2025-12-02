using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.WarehouseStores.Queries;

public class GetWarehouseStoreByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetWarehouseStoreByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WarehouseStoreDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<WarehouseStore>();
        var warehouseStore = await repo.GetByIdAsync(id);

        if (warehouseStore == null)
            return null;

        return new WarehouseStoreDto
        {
            Id = warehouseStore.Id,
            StoreId = warehouseStore.StoreId,
            ProductId = warehouseStore.ProductId,
            Quantity = warehouseStore.Quantity,
            CreatedAt = warehouseStore.CreatedAt,
            UpdatedAt = warehouseStore.UpdatedAt
        };
    }
}
