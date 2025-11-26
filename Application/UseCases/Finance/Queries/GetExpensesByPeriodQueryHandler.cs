using Application.DTOs.Finance;
using Application.Features.Finance.Queries;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Linq;

namespace Application.UseCases.Finance.Queries;

public class GetExpensesByPeriodQueryHandler : IRequestHandler<GetExpensesByPeriodQuery, IEnumerable<ExpenseDto>>
{
    private readonly IGeneralExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetExpensesByPeriodQueryHandler(IGeneralExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public Task<IEnumerable<ExpenseDto>> Handle(GetExpensesByPeriodQuery request, CancellationToken cancellationToken)
    {
        var items = _expenseRepository.GetQueryable()
            .Where(e => e.Date >= request.Start && e.Date <= request.End)
            .ToList();

        var result = _mapper.Map<IEnumerable<ExpenseDto>>(items);
        return Task.FromResult(result);
    }
}
