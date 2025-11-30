using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class CreateIncomeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateIncomeUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IncomeDto> ExecuteAsync(CreateIncomeDto dto)
        {
            var repo = _unitOfWork.GetRepository<GeneralIncome>();

            var entity = new GeneralIncome
            {
                Id = Guid.NewGuid(),
                ModuleId = dto.ModuleId,
                IncomeType = dto.IncomeType,
                Amount = dto.Amount,
                Date = dto.Date,
                Description = dto.Description,
                ReportId = dto.ReportId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new IncomeDto
            {
                Id = entity.Id,
                ModuleId = entity.ModuleId,
                IncomeType = entity.IncomeType,
                Amount = entity.Amount,
                Date = entity.Date,
                Description = entity.Description,
                ReportId = entity.ReportId
            };
        }
    }
}
