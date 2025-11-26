using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetActiveWarehousesQuery : IRequest<IEnumerable<WarehouseDto>>;

public class GetActiveWarehousesQueryHandler : IRequestHandler<GetActiveWarehousesQuery, IEnumerable<WarehouseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActiveWarehousesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WarehouseDto>> Handle(GetActiveWarehousesQuery request, CancellationToken cancellationToken)
    {
        var warehouses = await _unitOfWork.Warehouses.FindAsync(w => w.Status == true);
        return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
    }
}
