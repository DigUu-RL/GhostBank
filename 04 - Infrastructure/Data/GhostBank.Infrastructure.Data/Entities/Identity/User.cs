using GhostBank.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;

namespace GhostBank.Infrastructure.Data.Entities.Identity;

public class User : EntityBase, IPrincipal
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public UserRole Role { get; set; }

	// relationships
	public ICollection<UserClaim> Claims { get; set; } = [];

    [NotMapped]
	public IIdentity Identity => new GenericIdentity(Email);

	public bool IsInRole(string role)
	{
		throw new NotImplementedException();
	}

	public override string ToString()
	{
		return $"{Id}.{FirstName} {LastName}.{UserName}";
	}
}
