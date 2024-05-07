using Asp.Versioning;
using GhostBank.Application.DTOs;
using GhostBank.Application.DTOs.Authentication;
using GhostBank.Application.Interface.Authentication;
using GhostBank.Domain.Requests.Authentication;
using GhostBank.Domain.Requests.Identity;
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

	[HttpPost("sign-in")]
	public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
	{
		UserRequest result = await _authenticationService.GetUserAsync(request);

		AccessTokenDTO token = await _authenticationService.AuthenticateAsync(result, HttpContext);
		return Ok(token);
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
