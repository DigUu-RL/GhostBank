using GhostBank.Infrastructure.Middleware.Attributes;

namespace GhostBank.Domain.Requests;

[Request]
public class SignInRequest
{
    public Guid UserId { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}
