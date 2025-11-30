using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class CreateIncomeCommand
    {
        // DTO properties (model binding)
        public Guid ModuleId { get; set; }
        public string IncomeType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReportId { get; set; }

        private readonly IUnitOfWork _unitOfWork = null!;

        public CreateIncomeCommand() { }

        public CreateIncomeCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IncomeDto> ExecuteAsync(CreateIncomeCommand command)
        {
            var dto = new CreateIncomeDto
            {
                ModuleId = command.ModuleId,
                IncomeType = command.IncomeType,
                Description = command.Description,
                Amount = command.Amount,
                Date = command.Date,
                ReportId = command.ReportId
            };

            var useCase = new Application.UseCases.Finance.Commands.FinancialReports.CreateIncomeUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(dto);
        }
    }
}
