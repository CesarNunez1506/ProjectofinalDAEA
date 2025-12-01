using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales.Queries;

public class GetSalesByStoreUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesByStoreUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<List<SaleDto>> ExecuteAsync(Guid storeId)
    {
        var sales = await _saleRepository.GetByStoreIdAsync(storeId);

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
