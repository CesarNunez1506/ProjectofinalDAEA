using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SaleDetails.Queries;

public class GetSaleDetailByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleDetailByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDetailDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<SaleDetail>();
        var saleDetail = await repo.GetByIdAsync(id);

        if (saleDetail == null)
            return null;

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
