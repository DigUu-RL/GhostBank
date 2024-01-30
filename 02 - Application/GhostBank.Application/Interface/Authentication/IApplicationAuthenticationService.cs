using GhostBank.Domain.Requests.Authentication;
using Microsoft.AspNetCore.Http;

namespace GhostBank.Application.Interface.Authentication;

public interface IApplicationAuthenticationService
{
	Task<Guid> GetUserAsync(SignInRequest request);
	Task<string> AuthenticateAsync(SignInRequest request, HttpContext context);
}
