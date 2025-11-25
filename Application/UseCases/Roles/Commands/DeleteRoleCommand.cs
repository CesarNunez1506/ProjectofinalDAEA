using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Roles.Commands;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public DeleteRoleCommandHandler(
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role == null)
        {
            throw new InvalidOperationException("Rol no encontrado");
        }

        // Verificar que no haya usuarios con este rol
        var usersWithRole = await _userRepository.GetByRoleIdAsync(request.Id);
        if (usersWithRole.Any())
        {
            throw new InvalidOperationException("No se puede eliminar el rol porque tiene usuarios asociados");
        }

        await _roleRepository.DeleteAsync(request.Id);
        return true;
    }
}
