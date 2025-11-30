using Application.UseCases.Finance.Queries.FinancialReports;
using FluentValidation;

namespace Application.Validators.Finance
{
    public class GetFinancialReportByDateQueryValidator : AbstractValidator<GetFinancialReportByDateQuery>
    {
        public GetFinancialReportByDateQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date es requerido para buscar el reporte financiero.");
        }
    }
}
