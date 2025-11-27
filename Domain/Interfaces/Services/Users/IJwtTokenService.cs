using Domain.Entities;

namespace Domain.Interfaces.Services.Users;

public interface IJwtTokenService
{
    string GenerateToken(User user, string roleName);
    Guid? ValidateToken(string token);
}
