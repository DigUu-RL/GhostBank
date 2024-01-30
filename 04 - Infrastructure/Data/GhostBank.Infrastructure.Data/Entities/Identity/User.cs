using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Enums.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace GhostBank.Infrastructure.Data.Entities.Identity;

public class User : EntityBase, IPrincipal
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string CPF { get; set; } = null!;
	public string? CNPJ { get; set; }
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Cellphone { get; set; } = null!;
	public string Password { get; set; } = null!;
	public UserRole Role { get; set; }

	// relationships
	public Guid AddressId { get; set; }
	public Address Address { get; set; } = null!;
	public ICollection<UserClaim> Claims { get; set; } = [];
	public ICollection<Account> Accounts { get; set; } = [];

	[NotMapped]
	public bool IsAdministrator => Role is UserRole.Administrator;

	[NotMapped]
	public IIdentity Identity => new GenericIdentity(Email);

	public bool IsInRole(string role)
	{
		if (Enum.TryParse(role, out UserRole userRole))
			return userRole == Role;

		return false;
	}

	public override string ToString()
	{
		return $"{Id}.{UserName}";
	}
}
