using Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(UserRegistrationDto registerUserDto);

        Task<AuthResponse> LoginUser(UserLoginDto loginUserDto);

        bool IsUser(HttpContext context);

        bool IsAdmin(HttpContext context);

    }
}