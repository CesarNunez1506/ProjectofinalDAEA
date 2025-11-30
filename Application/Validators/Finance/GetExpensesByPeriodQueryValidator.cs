using Application.UseCases.Finance.Queries.FinancialReports;
using FluentValidation;

namespace Application.Validators.Finance
{
    public class GetExpensesByPeriodQueryValidator : AbstractValidator<GetExpensesByPeriodQuery>
    {
        public GetExpensesByPeriodQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate debe ser mayor o igual que StartDate.");
        }
    }
}
