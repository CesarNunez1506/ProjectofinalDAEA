using Application.DTOs.Inventory;
using FluentValidation;

namespace Application.Validators.Inventory;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del producto es requerido")
            .MaximumLength(200).WithMessage("El nombre no debe exceder 200 caracteres");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("La categoría es requerida");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("La descripción no debe exceder 1000 caracteres");
    }
}
