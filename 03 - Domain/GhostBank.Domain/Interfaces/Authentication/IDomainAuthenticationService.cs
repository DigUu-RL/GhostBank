using GhostBank.Domain.Requests.Authentication;
using Microsoft.AspNetCore.Http;

namespace GhostBank.Domain.Interfaces.Authentication;

public interface IDomainAuthenticationService
{
	Task<Guid> GetUserAsync(SignInRequest request);
	Task<string> AuthenticateAsync(SignInRequest request, HttpContext context);
}
