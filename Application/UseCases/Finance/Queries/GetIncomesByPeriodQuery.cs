using Application.DTOs.Finance;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UseCases.Finance.Queries;

public record GetIncomesByPeriodQuery(DateTime Start, DateTime End) : IRequest<IEnumerable<IncomeDto>>;
