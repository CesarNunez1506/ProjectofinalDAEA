using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Queries.FinancialReports
{
    public class GetFinancialReportByDateUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinancialReportByDateUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FinancialReportDto?> ExecuteAsync(DateTime date)
        {
            var repo = _unitOfWork.GetRepository<FinancialReport>();
            var report = await repo.FirstOrDefaultAsync(r => r.StartDate <= date && (r.EndDate == null || r.EndDate >= date));
            if (report == null) return null;

            return new FinancialReportDto
            {
                Id = report.Id,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpenses = report.TotalExpenses,
                NetProfit = report.NetProfit,
                Observations = report.Observations
            };
        }
    }
}
