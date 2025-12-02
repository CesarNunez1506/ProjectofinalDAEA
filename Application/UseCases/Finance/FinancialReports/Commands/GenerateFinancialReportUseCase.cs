using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Application.UseCases.Finance.FinancialReports.Commands;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.FinancialReports.Commands
{
    public class GenerateFinancialReportUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateFinancialReportUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FinancialReportDto> ExecuteAsync(GenerateFinancialReportDto dto)
        {
            var repo = _unitOfWork.GetRepository<FinancialReport>();

            var entity = new FinancialReport
            {
                Id = Guid.NewGuid(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Observations = dto.Observations,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new FinancialReportDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Observations = entity.Observations
            };
        }
    }
}
