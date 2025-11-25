using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Roles.Commands;

public record UpdateRoleCommand(Guid Id, UpdateRoleDto Dto) : IRequest<RoleDto>;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role == null)
        {
            throw new InvalidOperationException("Rol no encontrado");
        }

        _mapper.Map(request.Dto, role);
        
        var updatedRole = await _roleRepository.UpdateAsync(role);

        // Actualizar permisos si se especificaron
        if (request.Dto.PermissionIds != null)
        {
            // Validar que todos los permisos existan
            foreach (var permissionId in request.Dto.PermissionIds)
            {
                if (!await _permissionRepository.ExistsAsync(permissionId))
                {
                    throw new InvalidOperationException($"El permiso {permissionId} no existe");
                }
            }

            // Obtener permisos actuales
            var currentPermissions = await _roleRepository.GetPermissionsByRoleIdAsync(request.Id);
            var currentPermissionIds = currentPermissions.Select(p => p.Id).ToList();

            // Calcular permisos a agregar y eliminar
            var permissionsToAdd = request.Dto.PermissionIds.Except(currentPermissionIds).ToList();
            var permissionsToRemove = currentPermissionIds.Except(request.Dto.PermissionIds).ToList();

            if (permissionsToAdd.Any())
            {
                await _roleRepository.AddPermissionsAsync(request.Id, permissionsToAdd);
            }

            if (permissionsToRemove.Any())
            {
                await _roleRepository.RemovePermissionsAsync(request.Id, permissionsToRemove);
            }
        }

        // Recargar con permisos incluidos
        var roleWithPermissions = await _roleRepository.GetByIdAsync(updatedRole.Id);
        
        return _mapper.Map<RoleDto>(roleWithPermissions);
    }
}
