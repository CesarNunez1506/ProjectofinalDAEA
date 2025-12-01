using System;
using System.Threading.Tasks;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands
{
    public class RecordOverheadCommand
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        private readonly IUnitOfWork _unitOfWork = null!;

        public RecordOverheadCommand() { }

        public RecordOverheadCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OverheadDto> ExecuteAsync(RecordOverheadCommand command)
        {
            var dto = new CreateOverheadDto
            {
                Name = command.Name,
                Date = command.Date,
                Type = command.Type,
                Amount = command.Amount,
                Description = command.Description
            };

            var useCase = new Application.UseCases.Finance.Commands.RecordOverheadUseCase(_unitOfWork);
            return await useCase.ExecuteAsync(dto);
        }
    }
}
