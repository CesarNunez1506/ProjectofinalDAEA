using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record CreateWarehouseCommand(CreateWarehouseDto Dto) : IRequest<WarehouseDto>;

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, WarehouseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateWarehouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<WarehouseDto> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = _mapper.Map<Warehouse>(request.Dto);
        warehouse.Id = Guid.NewGuid();
        warehouse.Status = true;
        warehouse.CreatedAt = DateTime.UtcNow;
        warehouse.UpdatedAt = DateTime.UtcNow;

        var createdWarehouse = await _unitOfWork.Warehouses.AddAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<WarehouseDto>(createdWarehouse);
    }
}
