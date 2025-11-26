using Application.DTOs.Finance;
using MediatR;

namespace Application.Features.Finance.Commands;

public record CreateIncomeCommand(IncomeDto Income) : IRequest<IncomeDto>;
