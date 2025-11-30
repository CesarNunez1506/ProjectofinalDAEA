using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands.FinancialReports
{
    public class RecordOverheadUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecordOverheadUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OverheadDto> ExecuteAsync(CreateOverheadDto dto)
        {
            var repo = _unitOfWork.GetRepository<Overhead>();

            var entity = new Overhead
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Date = dto.Date,
                Type = dto.Type,
                Amount = dto.Amount,
                Description = dto.Description,
                Status = dto.Status ?? true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new OverheadDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Type = entity.Type,
                Amount = entity.Amount,
                Description = entity.Description,
                Status = entity.Status
            };
        }
    }
}
