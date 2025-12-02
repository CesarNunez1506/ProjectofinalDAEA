using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;
using System.Linq;

namespace Application.UseCases.Finance.FinancialReports.Queries
{
    public class GetProfitLossStatementUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfitLossStatementUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfitLossStatementDto> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var incomeRepo = _unitOfWork.GetRepository<GeneralIncome>();
            var expenseRepo = _unitOfWork.GetRepository<GeneralExpense>();

            var incomes = await incomeRepo.FindAsync(i => i.Date >= startDate && i.Date <= endDate);
            var expenses = await expenseRepo.FindAsync(e => e.Date >= startDate && e.Date <= endDate);

            var totalIncome = incomes.Sum(i => i.Amount);
            var totalExpenses = expenses.Sum(e => e.Amount);

            return new ProfitLossStatementDto
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                NetProfit = totalIncome - totalExpenses
            };
        }
    }
}
