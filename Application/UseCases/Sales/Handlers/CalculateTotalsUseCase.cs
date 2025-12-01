using Application.DTOs.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Cálculo independiente de totales (para validar antes de registrar).
/// </summary>
public class CalculateTotalsUseCase
{
    public SaleTotalsDto Execute(CreateSaleDto dto)
    {
        decimal subtotal = 0;

        foreach (var item in dto.Details)
            subtotal += item.Quantity * item.UnitPrice;

        decimal total = subtotal - dto.Discount + dto.Tax;

        return new SaleTotalsDto
        {
            Subtotal = subtotal,
            Total = total
        };
    }
}
