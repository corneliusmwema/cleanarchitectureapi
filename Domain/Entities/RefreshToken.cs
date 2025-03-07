namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int UserId { get; set; }
    public bool IsRevoked { get; set; } = false;
}