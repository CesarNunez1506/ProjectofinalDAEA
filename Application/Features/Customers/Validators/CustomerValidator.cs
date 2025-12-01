using FluentValidation;
using Domain.Entities;

namespace Application.Features.Customers.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CustomerName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.Email)
                .EmailAddress().When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Phone)
                .MaximumLength(20);
        }
    }
}
