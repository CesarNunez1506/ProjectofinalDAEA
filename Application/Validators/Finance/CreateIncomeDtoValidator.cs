using FluentValidation;
using Application.DTOs.Finance.Request;

namespace Application.Validators.Finance;

public class CreateIncomeDtoValidator : AbstractValidator<CreateIncomeDto>
{
    public CreateIncomeDtoValidator()
    {
        RuleFor(x => x.ModuleId).NotEmpty().WithMessage("ModuleId es requerido");
        RuleFor(x => x.IncomeType).NotEmpty().WithMessage("IncomeType es requerido").MaximumLength(200);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount debe ser mayor que 0");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Date es requerido");
    }
}
