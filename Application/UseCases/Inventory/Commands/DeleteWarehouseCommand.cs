using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record DeleteWarehouseCommand(Guid WarehouseId) : IRequest<bool>;

public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
        var warehouse = await warehouseRepo.FirstOrDefaultAsync(w => w.Id == request.WarehouseId);
        if (warehouse == null)
        {
            throw new Exception($"Warehouse with ID {request.WarehouseId} not found");
        }

        warehouse.Status = false;
        warehouse.UpdatedAt = DateTime.UtcNow;

        warehouseRepo.Update(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
