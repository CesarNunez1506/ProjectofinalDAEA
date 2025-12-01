using FluentValidation;
using Application.Features.Sales.Commands.CreateSale;

namespace Application.Features.Sales.Validators
{
    public class SaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public SaleValidator()
        {
            RuleFor(s => s.StoreId)
                .NotEmpty().WithMessage("La tienda es obligatoria.");

            RuleFor(s => s.Details)
                .NotEmpty().WithMessage("Debe incluir al menos un detalle.");

            RuleForEach(s => s.Details)
                .ChildRules(detail =>
                {
                    detail.RuleFor(d => d.ProductId)
                        .NotEmpty();

                    detail.RuleFor(d => d.Quantity)
                        .GreaterThan(0);

                    detail.RuleFor(d => d.Mount)
                        .GreaterThan(0);
                });
        }
    }
}
