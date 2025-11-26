using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Finance.Commands;

public class RecordOverheadCommandHandler : IRequestHandler<RecordOverheadCommand, OverheadDto>
{
    private readonly IOverheadRepository _overheadRepository;
    private readonly IMapper _mapper;

    public RecordOverheadCommandHandler(IOverheadRepository overheadRepository, IMapper mapper)
    {
        _overheadRepository = overheadRepository;
        _mapper = mapper;
    }

    public async Task<OverheadDto> Handle(RecordOverheadCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Overhead;
        var entity = new Overhead
        {
            Id = dto.Id ?? Guid.NewGuid(),
            Name = dto.Name,
            Amount = dto.Amount,
            Date = dto.Date,
            Description = dto.Description,
            Type = dto.Type,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _overheadRepository.AddAsync(entity);
        return _mapper.Map<OverheadDto>(created);
    }
}
