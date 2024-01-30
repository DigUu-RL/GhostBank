using GhostBank.Domain.Attributes;

namespace GhostBank.Domain.Requests.Authentication;

[Request]
public class SignInRequest
{
	public Guid UserId { get; set; }
	public string? Login { get; set; }
	public string? Password { get; set; }
}
