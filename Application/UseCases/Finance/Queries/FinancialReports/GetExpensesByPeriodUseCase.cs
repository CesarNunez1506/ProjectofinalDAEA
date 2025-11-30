using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Queries.FinancialReports
{
    public class GetExpensesByPeriodUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExpensesByPeriodUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ExpenseDto>> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var repo = _unitOfWork.GetRepository<GeneralExpense>();
            var items = await repo.FindAsync(e => e.Date >= startDate && e.Date <= endDate);

            return items.Select(e => new ExpenseDto
            {
                Id = e.Id,
                ModuleId = e.ModuleId,
                ExpenseType = e.ExpenseType,
                Amount = e.Amount,
                Date = e.Date,
                Description = e.Description,
                ReportId = e.ReportId
            }).ToList();
        }
    }
}
