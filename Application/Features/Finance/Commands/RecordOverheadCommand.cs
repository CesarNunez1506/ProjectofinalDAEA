using Application.DTOs.Finance;
using MediatR;

namespace Application.Features.Finance.Commands;

public record RecordOverheadCommand(OverheadDto Overhead) : IRequest<OverheadDto>;
