using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetWarehouseProductStockQuery(Guid WarehouseId, Guid ProductId) : IRequest<int>;

public class GetWarehouseProductStockQueryHandler : IRequestHandler<GetWarehouseProductStockQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetWarehouseProductStockQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(GetWarehouseProductStockQuery request, CancellationToken cancellationToken)
    {
        var warehouseProductRepo = _unitOfWork.GetRepository<WarehouseProduct>();
        var warehouseProduct = await warehouseProductRepo.FirstOrDefaultAsync(
            wp => wp.WarehouseId == request.WarehouseId && wp.ProductId == request.ProductId);
        return warehouseProduct?.Quantity ?? 0;
    }
}
