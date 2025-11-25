using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Roles.Commands;

public record CreateRoleCommand(CreateRoleDto Dto) : IRequest<RoleDto>;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request.Dto);
        role.Id = Guid.NewGuid();
        role.Status = true;

        var createdRole = await _roleRepository.CreateAsync(role);

        // Asignar permisos si se especificaron
        if (request.Dto.PermissionIds != null && request.Dto.PermissionIds.Any())
        {
            // Validar que todos los permisos existan
            foreach (var permissionId in request.Dto.PermissionIds)
            {
                if (!await _permissionRepository.ExistsAsync(permissionId))
                {
                    throw new InvalidOperationException($"El permiso {permissionId} no existe");
                }
            }

            await _roleRepository.AddPermissionsAsync(createdRole.Id, request.Dto.PermissionIds);
        }

        // Recargar con permisos incluidos
        var roleWithPermissions = await _roleRepository.GetByIdAsync(createdRole.Id);
        
        return _mapper.Map<RoleDto>(roleWithPermissions);
    }
}
