using GhostBank.Domain.Attributes;
using GhostBank.Infrastructure.Data.Enums.Identity;

namespace GhostBank.Domain.Requests.Identity;

[Request]
public class UserRequest
{
	public Guid? Id { get; set; }
	public string? Name { get; set; }
	public string? CPF { get; set; }
	public string? CNPJ { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? Cellphone { get; set; }
	public string? Password { get; set; }
	public UserRole Role { get; set; }
	public AddressRequest? Address { get; set; }
}
