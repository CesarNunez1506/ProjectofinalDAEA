using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public class GenerateFinancialReportCommandHandler : IRequestHandler<GenerateFinancialReportCommand, FinancialReportDto>
{
    private readonly IFinancialReportRepository _reportRepository;

    public GenerateFinancialReportCommandHandler(IFinancialReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<FinancialReportDto> Handle(GenerateFinancialReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GenerateReportAsync(request.Start, request.End);
        if (report == null) return null!;

        return new FinancialReportDto
        {
            Id = report.Id,
            StartDate = report.StartDate,
            EndDate = report.EndDate,
            TotalIncome = report.TotalIncome,
            TotalExpenses = report.TotalExpenses,
            NetProfit = report.NetProfit,
            Observations = report.Observations,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };
    }
}
