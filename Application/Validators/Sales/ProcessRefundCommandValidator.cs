using FluentValidation;

namespace Application.Features.Sales.Validators;

public class ProcessRefundCommandValidator : AbstractValidator<decimal>
{
    public ProcessRefundCommandValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0)
            .WithMessage("El monto del reembolso debe ser mayor que cero.");
    }
}
