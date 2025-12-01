using FluentValidation;

namespace Application.Features.Sales.Validators;

public class CancelSaleCommandValidator : AbstractValidator<Guid>
{
    public CancelSaleCommandValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Debe especificar un ID de venta.");
    }
}
