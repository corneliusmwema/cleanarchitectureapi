namespace Application.Dtos;

public class AuthResponse
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public TokenDto Token { get; set; } = new();
}