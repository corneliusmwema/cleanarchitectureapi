using Domain.Entities;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

        string GenerateRefreshToken();
    }
}