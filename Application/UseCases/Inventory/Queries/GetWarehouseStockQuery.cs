using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetWarehouseStockQuery(Guid WarehouseId) : IRequest<IEnumerable<WarehouseProductDto>>;

public class GetWarehouseStockQueryHandler : IRequestHandler<GetWarehouseStockQuery, IEnumerable<WarehouseProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetWarehouseStockQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WarehouseProductDto>> Handle(GetWarehouseStockQuery request, CancellationToken cancellationToken)
    {
        var warehouseProducts = await _unitOfWork.WarehouseProducts.FindAsync(
            wp => wp.WarehouseId == request.WarehouseId && wp.Status == true);
        return _mapper.Map<IEnumerable<WarehouseProductDto>>(warehouseProducts);
    }
}
