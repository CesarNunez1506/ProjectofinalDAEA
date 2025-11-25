using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Users;
using MediatR;

namespace Application.UseCases.Users.Commands;

public record CreateUserCommand(CreateUserDto Dto) : IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHashService passwordHashService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validar que el rol exista
        if (request.Dto.RoleId.HasValue && !await _roleRepository.ExistsAsync(request.Dto.RoleId.Value))
        {
            throw new InvalidOperationException("El rol especificado no existe");
        }

        // Validar que el email no esté en uso
        if (await _userRepository.ExistsByEmailAsync(request.Dto.Email))
        {
            throw new InvalidOperationException("El email ya está en uso");
        }

        // Validar que el DNI no esté en uso
        if (await _userRepository.ExistsByDniAsync(request.Dto.Dni))
        {
            throw new InvalidOperationException("El DNI ya está en uso");
        }

        var user = _mapper.Map<User>(request.Dto);
        user.Id = Guid.NewGuid();
        user.Password = _passwordHashService.HashPassword(request.Dto.Password);
        user.Status = true;

        var createdUser = await _userRepository.CreateAsync(user);
        
        // Recargar con el rol incluido
        var userWithRole = await _userRepository.GetByIdAsync(createdUser.Id);
        
        return _mapper.Map<UserDto>(userWithRole);
    }
}
