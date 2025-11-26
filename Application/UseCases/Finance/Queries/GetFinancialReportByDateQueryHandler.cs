using Application.DTOs.Finance;
using Application.Features.Finance.Queries;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Queries;

public class GetFinancialReportByDateQueryHandler : IRequestHandler<GetFinancialReportByDateQuery, FinancialReportDto>
{
    private readonly IFinancialReportRepository _reportRepository;

    public GetFinancialReportByDateQueryHandler(IFinancialReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<FinancialReportDto> Handle(GetFinancialReportByDateQuery request, CancellationToken cancellationToken)
    {
        // Attempt to generate or fetch a report for the period
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
