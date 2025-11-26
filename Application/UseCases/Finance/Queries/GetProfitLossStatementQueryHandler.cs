using Application.DTOs.Finance;
using Application.Features.Finance.Queries;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Queries;

public class GetProfitLossStatementQueryHandler : IRequestHandler<GetProfitLossStatementQuery, ProfitLossDto>
{
    private readonly IGeneralIncomeRepository _incomeRepository;
    private readonly IGeneralExpenseRepository _expenseRepository;

    public GetProfitLossStatementQueryHandler(IGeneralIncomeRepository incomeRepository, IGeneralExpenseRepository expenseRepository)
    {
        _incomeRepository = incomeRepository;
        _expenseRepository = expenseRepository;
    }

    public Task<ProfitLossDto> Handle(GetProfitLossStatementQuery request, CancellationToken cancellationToken)
    {
        var incomes = _incomeRepository.GetQueryable()
            .Where(i => i.Date >= request.Start && i.Date <= request.End)
            .ToList();

        var expenses = _expenseRepository.GetQueryable()
            .Where(e => e.Date >= request.Start && e.Date <= request.End)
            .ToList();

        var totalIncome = incomes.Sum(i => i.Amount);
        var totalExpenses = expenses.Sum(e => e.Amount);

        var result = new ProfitLossDto
        {
            StartDate = request.Start,
            EndDate = request.End,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            NetProfit = totalIncome - totalExpenses
        };

        return Task.FromResult(result);
    }
}
