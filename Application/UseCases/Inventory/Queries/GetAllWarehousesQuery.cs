using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetAllWarehousesQuery : IRequest<IEnumerable<WarehouseDto>>;

public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, IEnumerable<WarehouseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllWarehousesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WarehouseDto>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
        var warehouses = await warehouseRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
    }
}
