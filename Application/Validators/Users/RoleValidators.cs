using Application.DTOs.Users;
using FluentValidation;

namespace Application.Validators.Users;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(50).WithMessage("El nombre no debe exceder 50 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("La descripción no debe exceder 255 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}

public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(50).WithMessage("El nombre no debe exceder 50 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("La descripción no debe exceder 255 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
