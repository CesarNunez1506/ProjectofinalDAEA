using Application.DTOs.Finance;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public record RecordOverheadCommand(OverheadDto Overhead) : IRequest<OverheadDto>;
