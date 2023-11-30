﻿using Asp.Versioning;
using GhostBank.Application.Interface;
using GhostBank.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.V1;

[ApiController]
[ApiVersion(1)]
[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]/")]
public class AuthenticationController(IApplicationAuthenticationService authenticationService) : Controller
{
	private readonly IApplicationAuthenticationService _authenticationService = authenticationService;

	[HttpPost("signin")]
	public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
	{
		Guid userId = await _authenticationService.GetUserAsync(request);

		request.UserId = userId;

		string token = await _authenticationService.AuthenticateAsync(request, HttpContext);
		return Ok(token);
	}
}