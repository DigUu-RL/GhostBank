using Asp.Versioning;
using GhostBank.Application.DTOs;
using GhostBank.Application.Interface.Authentication;
using GhostBank.Domain.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using AuthorizeAttribute = GhostBank.Domain.Attributes.AuthorizeAttribute;

namespace GhostBank.Web.API.V1.Authentication;

[ApiController]
[ApiVersion(1)]
[AllowAnonymous]
[Route("api/[controller]/")]
public class AuthenticationController(
	IApplicationAuthenticationService authenticationService,
	IApplicationJwtService jwtService
) : Controller
{
	private readonly IApplicationAuthenticationService _authenticationService =
		authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

	private readonly IApplicationJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

	[HttpPost("signin")]
	public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
	{
		Guid userId = await _authenticationService.GetUserAsync(request);

		request.UserId = userId;

		string token = await _authenticationService.AuthenticateAsync(request, HttpContext);
		return Ok(new { token });
	}

	[Authorize]
	[HttpGet("me")]
	public async Task<IActionResult> Me()
	{
		StringValues token = Request.Headers[nameof(Authorization)];

		UserDTO user = await _jwtService.ValidateTokenAsync(token);
		return Ok(user);
	}
}
