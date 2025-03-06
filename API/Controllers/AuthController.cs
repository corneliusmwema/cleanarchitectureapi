using API.Common;
using Application.Dtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]

    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegistrationDto registerUserDto)
        {
            var result = await _authService.RegisterUserAsync(registerUserDto);

            return StatusCode(201, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto loginUserDto)
        {
            var result = await _authService.LoginUser(loginUserDto);

            return StatusCode(200, result);
        }


        [HttpGet("me")]
        [Authorize]

        public IActionResult IsUser()
        {
            var result = CurrentUser;

            return StatusCode(200, result);
        }
    }
}