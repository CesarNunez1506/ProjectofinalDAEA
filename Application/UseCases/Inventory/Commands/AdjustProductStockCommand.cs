using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
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
        var warehouseProduct = await _unitOfWork.WarehouseProducts.FindOneAsync(
            wp => wp.WarehouseId == request.Dto.WarehouseId && wp.ProductId == request.Dto.ProductId);

        if (warehouseProduct == null)
        {
            throw new Exception($"Product {request.Dto.ProductId} not found in warehouse {request.Dto.WarehouseId}");
        }

        warehouseProduct.Quantity = request.Dto.Quantity;
        warehouseProduct.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.WarehouseProducts.UpdateAsync(warehouseProduct);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
