using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SaleDetails.Queries;

public class GetAllSaleDetailsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSaleDetailsQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SaleDetailDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<SaleDetail>();
        var saleDetails = await repo.GetAllAsync();

        return saleDetails.Select(sd => new SaleDetailDto
        {
            Id = sd.Id,
            SaleId = sd.SaleId,
            ProductId = sd.ProductId,
            Quantity = sd.Quantity,
            Mount = sd.Mount,
            CreatedAt = sd.CreatedAt,
            UpdatedAt = sd.UpdatedAt
        });
    }
}
