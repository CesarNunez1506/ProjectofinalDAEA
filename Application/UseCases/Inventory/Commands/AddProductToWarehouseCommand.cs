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
        var warehouse = await _unitOfWork.Warehouses.FindOneAsync(w => w.Id == request.Dto.WarehouseId);
        if (warehouse == null)
        {
            throw new Exception($"Warehouse with ID {request.Dto.WarehouseId} not found");
        }

        var product = await _unitOfWork.Products.FindOneAsync(p => p.Id == request.Dto.ProductId);
        if (product == null)
        {
            throw new Exception($"Product with ID {request.Dto.ProductId} not found");
        }

        var warehouseProduct = await _unitOfWork.WarehouseProducts.FindOneAsync(
            wp => wp.WarehouseId == request.Dto.WarehouseId && wp.ProductId == request.Dto.ProductId);

        if (warehouseProduct != null)
        {
            warehouseProduct.Quantity += request.Dto.Quantity;
            warehouseProduct.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.WarehouseProducts.UpdateAsync(warehouseProduct);
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
            await _unitOfWork.WarehouseProducts.AddAsync(warehouseProduct);
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
