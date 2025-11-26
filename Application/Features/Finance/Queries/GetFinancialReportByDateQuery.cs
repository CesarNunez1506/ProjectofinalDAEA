using Application.DTOs.Finance;
using MediatR;
using System;

namespace Application.Features.Finance.Queries;

public record GetFinancialReportByDateQuery(DateTime Start, DateTime? End) : IRequest<FinancialReportDto>;
