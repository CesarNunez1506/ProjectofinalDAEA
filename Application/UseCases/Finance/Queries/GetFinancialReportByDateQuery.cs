using Application.DTOs.Finance;
using MediatR;
using System;

namespace Application.UseCases.Finance.Queries;

public record GetFinancialReportByDateQuery(DateTime Start, DateTime? End) : IRequest<FinancialReportDto>;
