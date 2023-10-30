using System.Security.Principal;

namespace GhostBank.Infrastructure.Data.Entities.Identity;

public class User : EntityBase, IIdentity
{
	// TODO: procurar documentação depois
	public string? AuthenticationType => throw new NotImplementedException();
	public bool IsAuthenticated => throw new NotImplementedException();
	public string? Name => throw new NotImplementedException();
}
