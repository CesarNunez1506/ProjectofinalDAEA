using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales.Queries;

public class GetDailySalesReportUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetDailySalesReportUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<List<DailySalesReportDto>> ExecuteAsync(DateTime date)
    {
        var sales = await _saleRepository.GetSalesByDayAsync(date);

        var total = sales.Sum(s => s.Total);
        var count = sales.Count;

        return new List<DailySalesReportDto>
        {
            new DailySalesReportDto
            {
                Date = date.Date,
                TotalSales = total,
                NumberOfSales = count
            }
        };
    }
}
