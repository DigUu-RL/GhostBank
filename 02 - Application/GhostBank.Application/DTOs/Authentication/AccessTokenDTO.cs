namespace GhostBank.Application.DTOs.Authentication;

public class AccessTokenDTO
{
    public DateTime ExpiresIn { get; set; }
    public string Token { get; set; } = null!;
}
