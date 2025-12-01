using Application.DTOs.Sales;
using FluentValidation;

namespace Application.Features.Sales.Validators;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleDto>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Details).NotEmpty();
        RuleForEach(x => x.Details).ChildRules(d =>
        {
            d.RuleFor(z => z.ProductId).NotEmpty();
            d.RuleFor(z => z.Quantity).GreaterThan(0);
            d.RuleFor(z => z.UnitPrice).GreaterThan(0);
        });
    }
}
