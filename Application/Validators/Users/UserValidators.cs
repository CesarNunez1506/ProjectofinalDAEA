using Application.DTOs.Users;
using FluentValidation;

namespace Application.Validators.Users;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres");

        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El DNI es requerido")
            .MaximumLength(20).WithMessage("El DNI no debe exceder 20 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100).WithMessage("El email no debe exceder 100 caracteres");

        RuleFor(x => x.Phonenumber)
            .MaximumLength(20).WithMessage("El teléfono no debe exceder 20 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Phonenumber));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("El rol es requerido");
    }
}

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres");

        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El DNI es requerido")
            .MaximumLength(20).WithMessage("El DNI no debe exceder 20 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100).WithMessage("El email no debe exceder 100 caracteres");

        RuleFor(x => x.Phonenumber)
            .MaximumLength(20).WithMessage("El teléfono no debe exceder 20 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Phonenumber));

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("El rol es requerido");
    }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("La contraseña actual es requerida");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("La nueva contraseña es requerida")
            .MinimumLength(6).WithMessage("La nueva contraseña debe tener al menos 6 caracteres");
    }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida");
    }
}
