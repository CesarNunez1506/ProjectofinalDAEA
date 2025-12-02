using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record UpdateWarehouseCommand(UpdateWarehouseDto Dto) : IRequest<WarehouseDto>;

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, WarehouseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateWarehouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<WarehouseDto> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
        var warehouse = await warehouseRepo.FirstOrDefaultAsync(w => w.Id == request.Dto.Id);
        if (warehouse == null)
        {
            throw new Exception($"Warehouse with ID {request.Dto.Id} not found");
        }

        warehouse.Name = request.Dto.Name;
        warehouse.Location = request.Dto.Location;
        warehouse.Capacity = request.Dto.Capacity;
        warehouse.Observation = request.Dto.Observation;
        warehouse.Status = request.Dto.Status;
        warehouse.UpdatedAt = DateTime.UtcNow;

        warehouseRepo.Update(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<WarehouseDto>(warehouse);
    }
}
