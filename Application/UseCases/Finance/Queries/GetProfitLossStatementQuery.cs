using Application.DTOs.Finance;
using MediatR;
using System;

namespace Application.UseCases.Finance.Queries;

public record GetProfitLossStatementQuery(DateTime Start, DateTime End) : IRequest<ProfitLossDto>;
