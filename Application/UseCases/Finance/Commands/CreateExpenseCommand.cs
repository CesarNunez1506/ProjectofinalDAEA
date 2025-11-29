using Application.DTOs.Finance;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public record CreateExpenseCommand(ExpenseDto Expense) : IRequest<ExpenseDto>;
