using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Expenses.Queries
{
    public class GetExpensesByPeriodQuery
    {
        private readonly IUnitOfWork _unitOfWork = null!;

        // Model-binding properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetExpensesByPeriodQuery() { }

        public GetExpensesByPeriodQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ExpenseDto>> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var useCase = new GetExpensesByPeriodUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(startDate, endDate);
        }
    }
}
