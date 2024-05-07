using GhostBank.Application.DTOs;
using GhostBank.Application.DTOs.Authentication;
using GhostBank.Domain.Requests.Authentication;
using GhostBank.Domain.Requests.Identity;
using Microsoft.AspNetCore.Http;

namespace GhostBank.Application.Interface.Authentication;

public interface IApplicationAuthenticationService
{
	Task<UserRequest> GetUserAsync(SignInRequest request);
	Task<AccessTokenDTO> AuthenticateAsync(UserRequest request, HttpContext context);
}
