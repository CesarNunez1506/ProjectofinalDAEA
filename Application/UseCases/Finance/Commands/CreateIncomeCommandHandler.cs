using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, IncomeDto>
{
    private readonly IGeneralIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public CreateIncomeCommandHandler(IGeneralIncomeRepository incomeRepository, IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _mapper = mapper;
    }

    public async Task<IncomeDto> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Income;
        var entity = new GeneralIncome
        {
            Id = dto.Id ?? Guid.NewGuid(),
            ModuleId = dto.ModuleId,
            IncomeType = dto.IncomeType,
            Amount = dto.Amount,
            Date = dto.Date,
            Description = dto.Description,
            ReportId = dto.ReportId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _incomeRepository.AddAsync(entity);
        return _mapper.Map<IncomeDto>(created);
    }
}
