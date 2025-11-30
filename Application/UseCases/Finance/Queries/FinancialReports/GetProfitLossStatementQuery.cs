using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Queries.FinancialReports
{
    public class GetProfitLossStatementQuery
    {
        private readonly IUnitOfWork _unitOfWork = null!;

        // Model-binding properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetProfitLossStatementQuery() { }

        public GetProfitLossStatementQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfitLossStatementDto> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            var useCase = new Application.UseCases.Finance.Queries.FinancialReports.GetProfitLossStatementUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(startDate, endDate);
        }
    }
}
