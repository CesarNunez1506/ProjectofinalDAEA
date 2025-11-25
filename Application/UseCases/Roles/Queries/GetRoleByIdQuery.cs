using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Roles.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleDto?>;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        return role != null ? _mapper.Map<RoleDto>(role) : null;
    }
}
