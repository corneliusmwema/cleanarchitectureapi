using Domain.Entities;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {

        Task<User?> GetUserByEmail(string email);

        Task<User> CreateUser(User user);

    }
}