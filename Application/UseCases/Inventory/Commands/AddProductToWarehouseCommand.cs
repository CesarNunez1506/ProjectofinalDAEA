using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record AddProductToWarehouseCommand(AddWarehouseProductDto Dto) : IRequest<bool>;

public class AddProductToWarehouseCommandHandler : IRequestHandler<AddProductToWarehouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductToWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AddProductToWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.GetRepository<Warehouse>();
        var warehouse = await warehouseRepo.FirstOrDefaultAsync(w => w.Id == request.Dto.WarehouseId);
        if (warehouse == null)
        {
            throw new Exception($"Warehouse with ID {request.Dto.WarehouseId} not found");
        }

        var productRepo = _unitOfWork.GetRepository<Product>();
        var product = await productRepo.FirstOrDefaultAsync(p => p.Id == request.Dto.ProductId);
        if (product == null)
        {
            throw new Exception($"Product with ID {request.Dto.ProductId} not found");
        }

        var warehouseProductRepo = _unitOfWork.GetRepository<WarehouseProduct>();
        var warehouseProduct = await warehouseProductRepo.FirstOrDefaultAsync(
            wp => wp.WarehouseId == request.Dto.WarehouseId && wp.ProductId == request.Dto.ProductId);

        if (warehouseProduct != null)
        {
            warehouseProduct.Quantity += request.Dto.Quantity;
            warehouseProduct.UpdatedAt = DateTime.UtcNow;
            warehouseProductRepo.Update(warehouseProduct);
        }
        else
        {
            warehouseProduct = new WarehouseProduct
            {
                Id = Guid.NewGuid(),
                WarehouseId = request.Dto.WarehouseId,
                ProductId = request.Dto.ProductId,
                Quantity = request.Dto.Quantity,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await warehouseProductRepo.AddAsync(warehouseProduct);
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
