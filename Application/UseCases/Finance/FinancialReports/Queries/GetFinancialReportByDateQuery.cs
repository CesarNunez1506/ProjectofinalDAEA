using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.FinancialReports.Queries
{
    public class GetFinancialReportByDateQuery
    {
        private readonly IUnitOfWork _unitOfWork = null!;

        // Model-binding property
        public DateTime Date { get; set; }

        public GetFinancialReportByDateQuery() { }

        public GetFinancialReportByDateQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FinancialReportDto?> ExecuteAsync(DateTime date)
        {
            var useCase = new GetFinancialReportByDateUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(date);
        }
    }
}
