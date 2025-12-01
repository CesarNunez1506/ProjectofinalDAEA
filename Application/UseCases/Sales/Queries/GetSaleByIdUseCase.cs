using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales.Queries;

public class GetSaleByIdUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetSaleByIdUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<SaleDto?> ExecuteAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale == null)
            return null;

        return new SaleDto
        {
            Id = sale.Id,
            StoreId = sale.StoreId,
            CustomerId = sale.CustomerId,
            Total = sale.Total,
            Status = sale.Status,
            CreatedAt = sale.CreatedAt,
            Details = sale.Details.Select(d => new SaleDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Subtotal = d.Subtotal
            }).ToList()
        };
    }
}
