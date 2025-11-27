using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Users.Queries;

public record GetUsersByRoleQuery(Guid RoleId) : IRequest<IEnumerable<UserDto>>;

public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersByRoleQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetByRoleIdAsync(request.RoleId);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
