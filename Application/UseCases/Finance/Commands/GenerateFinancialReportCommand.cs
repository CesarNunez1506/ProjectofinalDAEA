using Application.DTOs.Finance;
using MediatR;
using System;

namespace Application.UseCases.Finance.Commands;

public record GenerateFinancialReportCommand(DateTime Start, DateTime? End) : IRequest<FinancialReportDto>;