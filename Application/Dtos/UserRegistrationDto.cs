



using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class UserRegistrationDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        //should be a file
        public IFormFile? ProfilePhoto { get; set; } = null;
    }
}