using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record AdjustProductStockCommand(AdjustStockDto Dto) : IRequest<bool>;

public class AdjustProductStockCommandHandler : IRequestHandler<AdjustProductStockCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AdjustProductStockCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AdjustProductStockCommand request, CancellationToken cancellationToken)
    {
        var warehouseProductRepo = _unitOfWork.GetRepository<WarehouseProduct>();
        var warehouseProduct = await warehouseProductRepo.FirstOrDefaultAsync(
            wp => wp.WarehouseId == request.Dto.WarehouseId && wp.ProductId == request.Dto.ProductId);

        if (warehouseProduct == null)
        {
            throw new Exception($"Product {request.Dto.ProductId} not found in warehouse {request.Dto.WarehouseId}");
        }

        warehouseProduct.Quantity = request.Dto.Quantity;
        warehouseProduct.UpdatedAt = DateTime.UtcNow;

        warehouseProductRepo.Update(warehouseProduct);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
