using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Sales.Queries;

public class GetAllSalesQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSalesQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SaleDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<Sale>();
        var sales = await repo.GetAllAsync();

        return sales.Select(s => new SaleDto
        {
            Id = s.Id,
            IncomeDate = s.IncomeDate,
            StoreId = s.StoreId,
            TotalIncome = s.TotalIncome,
            Observations = s.Observations,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        });
    }
}
