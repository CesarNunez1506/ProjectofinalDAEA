using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Sales.Queries;

public class GetSaleByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Sale>();
        var sale = await repo.GetByIdAsync(id);

        if (sale == null)
            return null;

        return new SaleDto
        {
            Id = sale.Id,
            IncomeDate = sale.IncomeDate,
            StoreId = sale.StoreId,
            TotalIncome = sale.TotalIncome,
            Observations = sale.Observations,
            CreatedAt = sale.CreatedAt,
            UpdatedAt = sale.UpdatedAt
        };
    }
}
