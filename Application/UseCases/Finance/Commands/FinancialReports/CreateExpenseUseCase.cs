using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class CreateExpenseUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExpenseUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExpenseDto> ExecuteAsync(CreateExpenseDto dto)
        {
            var repo = _unitOfWork.GetRepository<GeneralExpense>();

            var entity = new GeneralExpense
            {
                Id = Guid.NewGuid(),
                ModuleId = dto.ModuleId,
                ExpenseType = dto.ExpenseType,
                Amount = dto.Amount,
                Date = dto.Date,
                Description = dto.Description,
                ReportId = dto.ReportId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new ExpenseDto
            {
                Id = entity.Id,
                ModuleId = entity.ModuleId,
                ExpenseType = entity.ExpenseType,
                Amount = entity.Amount,
                Date = entity.Date,
                Description = entity.Description,
                ReportId = entity.ReportId
            };
        }
    }
}
