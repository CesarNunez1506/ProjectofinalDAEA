using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Incomes.Queries
{
    public class GetIncomesByPeriodUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIncomesByPeriodUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<IncomeDto>> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var repo = _unitOfWork.GetRepository<GeneralIncome>();
            var items = await repo.FindAsync(i => i.Date >= startDate && i.Date <= endDate);

            return items.Select(i => new IncomeDto
            {
                Id = i.Id,
                ModuleId = i.ModuleId,
                IncomeType = i.IncomeType,
                Amount = i.Amount,
                Date = i.Date,
                Description = i.Description,
                ReportId = i.ReportId
            }).ToList();
        }
    }
}
