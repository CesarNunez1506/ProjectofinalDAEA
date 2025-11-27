using Application.DTOs.Users;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Users;
using MediatR;

namespace Application.UseCases.Users.Commands;

public record ChangePasswordCommand(Guid UserId, ChangePasswordDto Dto) : IRequest<bool>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("Usuario no encontrado");
        }

        // Verificar que la contraseña actual sea correcta
        if (!_passwordHashService.VerifyPassword(request.Dto.CurrentPassword, user.Password))
        {
            throw new InvalidOperationException("La contraseña actual es incorrecta");
        }

        // Actualizar con la nueva contraseña hasheada
        user.Password = _passwordHashService.HashPassword(request.Dto.NewPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }
}
