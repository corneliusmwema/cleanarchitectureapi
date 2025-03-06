namespace Application.Dtos
{
    public class TokenDto
    {
        public string TokenType { get; set; } = "Bearer";

        public string Token { get; set; }
    }
}