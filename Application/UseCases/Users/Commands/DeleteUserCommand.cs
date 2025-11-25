using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Users.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new InvalidOperationException("Usuario no encontrado");
        }

        await _userRepository.DeleteAsync(request.Id);
        return true;
    }
}
