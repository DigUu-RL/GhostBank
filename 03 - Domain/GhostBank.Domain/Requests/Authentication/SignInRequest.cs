using GhostBank.Domain.Attributes;

namespace GhostBank.Domain.Requests.Authentication;

[Request]
public class SignInRequest
{
	public string? Login { get; set; }
	public string? Password { get; set; }
}
