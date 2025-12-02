using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SaleDetails.Commands;

public class UpdateSaleDetailCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleDetailCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDetailDto> ExecuteAsync(Guid id, UpdateSaleDetailDto dto)
    {
        var repo = _unitOfWork.GetRepository<SaleDetail>();

        var saleDetail = await repo.GetByIdAsync(id);
        if (saleDetail == null)
            throw new KeyNotFoundException($"Detalle de venta con ID {id} no encontrado");

        saleDetail.SaleId = dto.SaleId;
        saleDetail.ProductId = dto.ProductId;
        saleDetail.Quantity = dto.Quantity;
        saleDetail.Mount = dto.Mount;
        saleDetail.UpdatedAt = DateTime.UtcNow;

        repo.Update(saleDetail);
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
