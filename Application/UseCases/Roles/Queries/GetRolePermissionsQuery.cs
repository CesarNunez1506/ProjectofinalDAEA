using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Roles.Queries;

public record GetRolePermissionsQuery(Guid RoleId) : IRequest<IEnumerable<PermissionDto>>;

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRolePermissionsQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _roleRepository.GetPermissionsByRoleIdAsync(request.RoleId);
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}
