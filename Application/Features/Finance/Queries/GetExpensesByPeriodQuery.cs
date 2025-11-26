using Application.DTOs.Finance;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Features.Finance.Queries;

public record GetExpensesByPeriodQuery(DateTime Start, DateTime End) : IRequest<IEnumerable<ExpenseDto>>;
