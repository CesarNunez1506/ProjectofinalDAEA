using Application.DTOs.Inventory;
using FluentValidation;

namespace Application.Validators.Inventory;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierDto>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Ruc)
            .GreaterThan(0).WithMessage("El RUC es requerido")
            .Must(BeValidRuc).WithMessage("El RUC debe tener 11 dígitos");

        RuleFor(x => x.SuplierName)
            .NotEmpty().WithMessage("El nombre del proveedor es requerido")
            .MaximumLength(200).WithMessage("El nombre no debe exceder 200 caracteres");

        RuleFor(x => x.ContactName)
            .NotEmpty().WithMessage("El nombre de contacto es requerido")
            .MaximumLength(150).WithMessage("El nombre de contacto no debe exceder 150 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido");

        RuleFor(x => x.Phone)
            .GreaterThan(0).WithMessage("El teléfono es requerido");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección es requerida")
            .MaximumLength(300).WithMessage("La dirección no debe exceder 300 caracteres");
    }

    private bool BeValidRuc(long ruc)
    {
        return ruc.ToString().Length == 11;
    }
}
