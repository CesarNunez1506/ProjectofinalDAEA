using System;
using System.Threading.Tasks;
using Application.DTOs.Finance;
using Application.UseCases.Finance.Overheads.Commands;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.FinancialReports.Commands
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

            var overhead = new Overhead
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

            await repo.AddAsync(overhead);
            await _unitOfWork.SaveChangesAsync();

            return new OverheadDto
            {
                Id = overhead.Id,
                Name = overhead.Name,
                Date = overhead.Date,
                Type = overhead.Type,
                Amount = overhead.Amount,
                Description = overhead.Description,
                Status = overhead.Status
            };
        }
    }
}
