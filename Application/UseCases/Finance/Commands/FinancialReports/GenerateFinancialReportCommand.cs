using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class GenerateFinancialReportCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Observations { get; set; }

        private readonly IUnitOfWork _unitOfWork = null!;

        public GenerateFinancialReportCommand() { }

        public GenerateFinancialReportCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FinancialReportDto> ExecuteAsync(GenerateFinancialReportCommand command)
        {
            var dto = new GenerateFinancialReportDto
            {
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Observations = command.Observations
            };

            var useCase = new Application.UseCases.Finance.Commands.FinancialReports.GenerateFinancialReportUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(dto);
        }
    }
}
