using Application.DTOs.Users;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Users;
using MediatR;

namespace Application.UseCases.Auth.Commands;

public record LoginCommand(LoginDto Dto) : IRequest<LoginResponseDto>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Buscar usuario por email
        var user = await _userRepository.GetByEmailAsync(request.Dto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        // Verificar que el usuario esté activo
        if (!user.Status)
        {
            throw new UnauthorizedAccessException("Usuario inactivo");
        }

        // Verificar contraseña
        if (!_passwordHashService.VerifyPassword(request.Dto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        // Generar token JWT
        var roleName = user.Role?.Name ?? "Sin Rol";
        var token = _jwtTokenService.GenerateToken(user, roleName);

        // Mapear permisos del rol
        RoleDto? roleDto = null;
        if (user.Role != null)
        {
            roleDto = new RoleDto
            {
                Id = user.Role.Id,
                Name = user.Role.Name,
                Description = user.Role.Description,
                Status = user.Role.Status,
                CreatedAt = user.Role.CreatedAt,
                UpdatedAt = user.Role.UpdatedAt ?? user.Role.CreatedAt,
                Permissions = user.Role.RolesPermissions.Select(rp => new PermissionDto
                {
                    Id = rp.Permission.Id,
                    ModuleId = rp.Permission.ModuleId,
                    ModuleName = rp.Permission.Module?.Name,
                    CanRead = rp.Permission.CanRead,
                    CanWrite = rp.Permission.CanWrite,
                    CanEdit = rp.Permission.CanEdit,
                    CanDelete = rp.Permission.CanDelete,
                    CreatedAt = rp.Permission.CreatedAt,
                    UpdatedAt = rp.Permission.UpdatedAt
                }).ToList()
            };
        }

        return new LoginResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Dni = user.Dni,
                Phonenumber = user.Phonenumber,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = roleName,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Role = roleDto
            }
        };
    }
}
