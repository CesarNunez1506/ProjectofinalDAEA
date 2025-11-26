using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly IGeneralExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public CreateExpenseCommandHandler(IGeneralExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Expense;
        var entity = new GeneralExpense
        {
            Id = dto.Id ?? Guid.NewGuid(),
            ModuleId = dto.ModuleId,
            ExpenseType = dto.ExpenseType,
            Amount = dto.Amount,
            Date = dto.Date,
            Description = dto.Description,
            ReportId = dto.ReportId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _expenseRepository.AddAsync(entity);
        return _mapper.Map<ExpenseDto>(created);
    }
}
