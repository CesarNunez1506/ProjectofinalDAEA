using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales.Queries;

public class GetSalesByDateRangeUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesByDateRangeUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<List<SaleDto>> ExecuteAsync(DateTime start, DateTime end)
    {
        var sales = await _saleRepository.GetByDateRangeAsync(start, end);

        return sales.Select(s => new SaleDto
        {
            Id = s.Id,
            StoreId = s.StoreId,
            CustomerId = s.CustomerId,
            Total = s.Total,
            Status = s.Status,
            CreatedAt = s.CreatedAt
        }).ToList();
    }
}
