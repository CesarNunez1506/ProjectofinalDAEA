using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Expenses.Commands
{
    public class CreateExpenseCommand
    {
        public Guid ModuleId { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReportId { get; set; }

        private readonly IUnitOfWork _unitOfWork = null!;

        public CreateExpenseCommand() { }

        public CreateExpenseCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExpenseDto> ExecuteAsync(CreateExpenseCommand command)
        {
            var dto = new CreateExpenseDto
            {
                ModuleId = command.ModuleId,
                ExpenseType = command.ExpenseType,
                Description = command.Description,
                Amount = command.Amount,
                Date = command.Date,
                ReportId = command.ReportId
            };

            var useCase = new CreateExpenseUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(dto);
        }
    }
}
