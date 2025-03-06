


using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;


        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)

        {
            if (context.User.Identity?.IsAuthenticated == true)
            {

                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var emailClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var nameClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


                if (userIdClaim != null && emailClaim != null)
                {

                    if (Guid.TryParse(userIdClaim, out var userId))
                    {
                        var user = new CurrentUser
                        {
                            UserId = userId,
                            Email = emailClaim,
                            UserName = nameClaim ?? string.Empty
                        };
                        context.Items["CurrentUser"] = user;
                    }

                }


            }

            await _next(context);
        }
    }
}