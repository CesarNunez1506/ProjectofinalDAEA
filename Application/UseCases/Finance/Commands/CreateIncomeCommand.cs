using Application.DTOs.Finance;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public record CreateIncomeCommand(IncomeDto Income) : IRequest<IncomeDto>;
