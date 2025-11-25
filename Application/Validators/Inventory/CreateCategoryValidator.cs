using Application.DTOs.Inventory;
using FluentValidation;

namespace Application.Validators.Inventory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es requerido")
            .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no debe exceder 500 caracteres");
    }
}
