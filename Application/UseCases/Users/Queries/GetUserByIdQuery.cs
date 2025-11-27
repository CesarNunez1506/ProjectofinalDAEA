using Application.DTOs.Users;
using AutoMapper;
using Domain.Interfaces.Repositories.Users;
using MediatR;

namespace Application.UseCases.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }
}
