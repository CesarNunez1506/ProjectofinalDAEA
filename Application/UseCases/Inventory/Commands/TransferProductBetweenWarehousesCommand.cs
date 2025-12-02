using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record TransferProductBetweenWarehousesCommand(TransferProductDto Dto) : IRequest<bool>;

public class TransferProductBetweenWarehousesCommandHandler : IRequestHandler<TransferProductBetweenWarehousesCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransferProductBetweenWarehousesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(TransferProductBetweenWarehousesCommand request, CancellationToken cancellationToken)
    {
        var warehouseProductRepo = _unitOfWork.GetRepository<WarehouseProduct>();
        var sourceWarehouseProduct = await warehouseProductRepo.FirstOrDefaultAsync(
            wp => wp.WarehouseId == request.Dto.SourceWarehouseId && wp.ProductId == request.Dto.ProductId);

        if (sourceWarehouseProduct == null || sourceWarehouseProduct.Quantity < request.Dto.Quantity)
        {
            throw new Exception("Insufficient stock in source warehouse");
        }

        sourceWarehouseProduct.Quantity -= request.Dto.Quantity;
        sourceWarehouseProduct.UpdatedAt = DateTime.UtcNow;
        warehouseProductRepo.Update(sourceWarehouseProduct);

        var destinationWarehouseProduct = await warehouseProductRepo.FirstOrDefaultAsync(
            wp => wp.WarehouseId == request.Dto.DestinationWarehouseId && wp.ProductId == request.Dto.ProductId);

        if (destinationWarehouseProduct != null)
        {
            destinationWarehouseProduct.Quantity += request.Dto.Quantity;
            destinationWarehouseProduct.UpdatedAt = DateTime.UtcNow;
            warehouseProductRepo.Update(destinationWarehouseProduct);
        }
        else
        {
            var newWarehouseProduct = new WarehouseProduct
            {
                Id = Guid.NewGuid(),
                WarehouseId = request.Dto.DestinationWarehouseId,
                ProductId = request.Dto.ProductId,
                Quantity = request.Dto.Quantity,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await warehouseProductRepo.AddAsync(newWarehouseProduct);
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
