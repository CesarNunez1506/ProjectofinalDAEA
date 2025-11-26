using Application.DTOs.Finance;
using Application.Features.Finance.Queries;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Linq;

namespace Application.UseCases.Finance.Queries;

public class GetIncomesByPeriodQueryHandler : IRequestHandler<GetIncomesByPeriodQuery, IEnumerable<IncomeDto>>
{
    private readonly IGeneralIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public GetIncomesByPeriodQueryHandler(IGeneralIncomeRepository incomeRepository, IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _mapper = mapper;
    }

    public Task<IEnumerable<IncomeDto>> Handle(GetIncomesByPeriodQuery request, CancellationToken cancellationToken)
    {
        var items = _incomeRepository.GetQueryable()
            .Where(i => i.Date >= request.Start && i.Date <= request.End)
            .ToList();

        var result = _mapper.Map<IEnumerable<IncomeDto>>(items);
        return Task.FromResult(result);
    }
}
