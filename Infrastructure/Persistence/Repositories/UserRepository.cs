

using Application.Interfaces.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly VideoDBContext _context;


        public UserRepository(VideoDBContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<User> CreateUser(User user)
        {

            var newUser = await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return newUser.Entity;
        }
    }
}