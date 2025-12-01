using Application.DTOs.Sales;
using FluentValidation;

namespace Application.Features.Sales.Validators;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleDto>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Tax).GreaterThanOrEqualTo(0);
    }
}
