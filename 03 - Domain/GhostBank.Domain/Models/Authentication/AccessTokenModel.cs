namespace GhostBank.Domain.Models.Authentication;

public class AccessTokenModel
{
	public DateTime ExpiresIn { get; set; }
	public string Token { get; set; } = null!;
}
