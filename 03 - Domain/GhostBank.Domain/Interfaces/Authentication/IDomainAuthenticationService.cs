using GhostBank.Domain.Models.Authentication;
using GhostBank.Domain.Requests.Authentication;
using GhostBank.Domain.Requests.Identity;
using Microsoft.AspNetCore.Http;

namespace GhostBank.Domain.Interfaces.Authentication;

public interface IDomainAuthenticationService
{
	Task<UserRequest> GetUserAsync(SignInRequest request);
	Task<AccessTokenModel> AuthenticateAsync(UserRequest request, HttpContext context);
}
