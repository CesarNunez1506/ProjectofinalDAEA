using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Users.Commands;

public record UpdateUserCommand(Guid Id, UpdateUserDto Dto) : IRequest<UserDto>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new InvalidOperationException("Usuario no encontrado");
        }

        // Validar que el rol exista
        if (request.Dto.RoleId.HasValue && !await _roleRepository.ExistsAsync(request.Dto.RoleId.Value))
        {
            throw new InvalidOperationException("El rol especificado no existe");
        }

        // Validar que el email no esté en uso por otro usuario
        if (!string.IsNullOrEmpty(request.Dto.Email) && await _userRepository.ExistsByEmailAsync(request.Dto.Email, request.Id))
        {
            throw new InvalidOperationException("El email ya está en uso");
        }

        // Validar que el DNI no esté en uso por otro usuario
        if (!string.IsNullOrEmpty(request.Dto.Dni) && await _userRepository.ExistsByDniAsync(request.Dto.Dni, request.Id))
        {
            throw new InvalidOperationException("El DNI ya está en uso");
        }

        _mapper.Map(request.Dto, user);
        
        var updatedUser = await _userRepository.UpdateAsync(user);
        
        // Recargar con el rol incluido
        var userWithRole = await _userRepository.GetByIdAsync(updatedUser.Id);
        
        return _mapper.Map<UserDto>(userWithRole);
    }
}
