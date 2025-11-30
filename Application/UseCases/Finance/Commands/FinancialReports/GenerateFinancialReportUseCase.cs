using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class GenerateFinancialReportUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateFinancialReportUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FinancialReportDto> ExecuteAsync(GenerateFinancialReportDto dto)
        {
            // Convertimos fechas a UTC para que PostgreSQL no falle
            var startDateUtc = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc);
            var endDateUtc = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc);

            var incomeRepo = _unitOfWork.GetRepository<GeneralIncome>();
            var expenseRepo = _unitOfWork.GetRepository<GeneralExpense>();
            var reportRepo = _unitOfWork.GetRepository<FinancialReport>();

            var incomes = (await incomeRepo.FindAsync(i => i.Date >= startDateUtc && i.Date <= endDateUtc)).ToList();
            var expenses = (await expenseRepo.FindAsync(e => e.Date >= startDateUtc && e.Date <= endDateUtc)).ToList();

            var totalIncome = incomes.Sum(i => i.Amount);
            var totalExpenses = expenses.Sum(e => e.Amount);
            var netProfit = totalIncome - totalExpenses;

            var report = new FinancialReport
            {
                Id = Guid.NewGuid(),
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                NetProfit = netProfit,
                Observations = dto.Observations,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await reportRepo.AddAsync(report);
            await _unitOfWork.SaveChangesAsync();

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
