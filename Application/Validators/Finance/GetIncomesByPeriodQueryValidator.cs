using Application.UseCases.Finance.Queries.FinancialReports;
using FluentValidation;

namespace Application.Validators.Finance
{
    public class GetIncomesByPeriodQueryValidator : AbstractValidator<GetIncomesByPeriodQuery>
    {
        public GetIncomesByPeriodQueryValidator()
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
