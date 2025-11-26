using FluentValidation;
using Application.DTOs.Finance.Request;

namespace Application.Validators.Finance;

public class UpdateOverheadDtoValidator : AbstractValidator<UpdateOverheadDto>
{
    public UpdateOverheadDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id es requerido");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name es requerido").MaximumLength(255);
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type es requerido").MaximumLength(200);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount debe ser mayor que 0");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Date es requerido");
    }
}
