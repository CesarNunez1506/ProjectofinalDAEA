using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SaleDetails.Commands;

public class CreateSaleDetailCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleDetailCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDetailDto> ExecuteAsync(CreateSaleDetailDto dto)
    {
        var repo = _unitOfWork.GetRepository<SaleDetail>();

        // Validar que la venta existe
        var saleRepo = _unitOfWork.GetRepository<Sale>();
        var saleExists = await saleRepo.AnyAsync(s => s.Id == dto.SaleId);
        if (!saleExists)
            throw new KeyNotFoundException($"Venta con ID {dto.SaleId} no encontrada");

        // Validar que el producto existe
        var productRepo = _unitOfWork.GetRepository<Product>();
        var productExists = await productRepo.AnyAsync(p => p.Id == dto.ProductId);
        if (!productExists)
            throw new KeyNotFoundException($"Producto con ID {dto.ProductId} no encontrado");

        var saleDetail = new SaleDetail
        {
            Id = Guid.NewGuid(),
            SaleId = dto.SaleId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Mount = dto.Mount,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(saleDetail);
        await _unitOfWork.SaveChangesAsync();

        return new SaleDetailDto
        {
            Id = saleDetail.Id,
            SaleId = saleDetail.SaleId,
            ProductId = saleDetail.ProductId,
            Quantity = saleDetail.Quantity,
            Mount = saleDetail.Mount,
            CreatedAt = saleDetail.CreatedAt,
            UpdatedAt = saleDetail.UpdatedAt
        };
    }
}
