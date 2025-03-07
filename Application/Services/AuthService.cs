using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Application.Utilities;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly ILogger<AuthService> _logger;

    private readonly SavePhoto _savePhoto;

    private readonly IUserRepository _userRepository;


    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public bool IsAdmin(HttpContext context)
    {
        return context.User.IsInRole("Admin");
    }

    public bool IsUser(HttpContext context)
    {
        return context.User.IsInRole("User");
    }

    public async Task<AuthResponse> LoginUser(UserLoginDto loginUserDto)
    {
        var userExists = await _userRepository.GetUserByEmail(loginUserDto.Email);

        if (userExists == null)
        {
            _logger.LogError("User provided does not exist :{User}", loginUserDto.Email);
            throw new UnauthorizedAccess("Invalid Credentials");
        }


        var checkPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, userExists.Password);

        if (!checkPassword)
        {
            _logger.LogError("User provided invalid password :{User}", loginUserDto.Email);
            throw new UnauthorizedAccess("Invalid Credentials");
        }


        var token = _jwtTokenGenerator.GenerateToken(userExists);

        return new AuthResponse
        {
            UserName = userExists.UserName,
            Email = userExists.Email,
            Token = { Token = token }
        };
    }

    public async Task<AuthResponse> RegisterUserAsync(UserRegistrationDto registerUserDto)
    {
        var user = await _userRepository.GetUserByEmail(registerUserDto.Email);

        if (user != null)
        {
            _logger.LogError("User already exists :{User}", registerUserDto.Email);
            throw new UnauthorizedAccess("User already exists");
        }

        var newUser = new User
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.UserName,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
        };

        var createdUser = await _userRepository.CreateUser(newUser);

        var token = _jwtTokenGenerator.GenerateToken(createdUser);

        return new AuthResponse
        {
            UserName = createdUser.UserName,
            Email = createdUser.Email,
            Token = { Token = token }
        };
    }


    // public async Task<AuthResponse> RefreshTokenAsync(string token)
    // {
    //     var user = await _userRepository.GetUserByToken(token);

    //     if (user == null)
    //     {
    //         _logger.LogError("User does not exist :{User}", token);
    //         throw new UnauthorizedAccess("Invalid Token");
    //     }

    //     var newToken = _jwtTokenGenerator.GenerateToken(user);

    //     return new AuthResponse
    //     {
    //         UserName = user.UserName,
    //         Email = user.Email,
    //         Token = {Token = newToken}
    //     };
    // }


    public Task<string> SaveImage(IFormFile file)
    {
        List<string> allowedFileExt = new List<string>
            { ".PNG", ".png", ".jpg", ".jpeg", ".tiff", ".gif", ".JPEG", ".JPG", ".TIFF", ".GIF" };

        var ext = Path.GetExtension(file.FileName);

        if (!allowedFileExt.Contains(ext))
        {
            _logger.LogError("Invalid file extension :{File}", file.FileName);
            throw new InvalidFileExtension("Unsupported file extension");
        }

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

        using (var stream = file.OpenReadStream())
        {
            var photoSection = "ProfilePhotos";
        }


        return Task.FromResult(fileName);
    }
}