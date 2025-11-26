using Application.DTOs.Finance;
using MediatR;

namespace Application.Features.Finance.Commands;

public record CreateExpenseCommand(ExpenseDto Expense) : IRequest<ExpenseDto>;
