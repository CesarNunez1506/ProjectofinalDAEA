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
        var stock = await _unitOfWork.Warehouses.GetProductStockAsync(request.WarehouseId, request.ProductId);
        return stock;
    }
}
