using GhostBank.Infrastructure.Data.Enums;
using GhostBank.Infrastructure.Middleware.Attributes;

namespace GhostBank.Domain.Requests;

[Request]
public class UserRequest
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public List<UserRole> Roles { get; set; } = [];
}
