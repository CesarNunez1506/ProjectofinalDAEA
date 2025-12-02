using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Incomes.Queries
{
    public class GetIncomesByPeriodQuery
    {
        private readonly IUnitOfWork _unitOfWork = null!;

        // Model-binding properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetIncomesByPeriodQuery() { }

        public GetIncomesByPeriodQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<IncomeDto>> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var useCase = new GetIncomesByPeriodUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(startDate, endDate);
        }
    }
}
