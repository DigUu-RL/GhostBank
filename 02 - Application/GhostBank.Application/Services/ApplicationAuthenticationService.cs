using GhostBank.Application.Interface;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces;
using GhostBank.Domain.Requests;
using GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net;
using InvalidDataException = GhostBank.Infrastructure.Middleware.Exceptions.Abstractions.InvalidDataException;

namespace GhostBank.Application.Services;

public class ApplicationAuthenticationService(IDomainAuthenticationService authenticationService) : IApplicationAuthenticationService
{
	private readonly IDomainAuthenticationService _authenticationService = authenticationService;

	public async Task<string> AuthenticateAsync(SignInRequest request, HttpContext context)
	{
		string token = await _authenticationService.AuthenticateAsync(request, context);
		return token;
	}

	public async Task<Guid> GetUserAsync(SignInRequest request)
	{
		if (string.IsNullOrEmpty(request.Login))
			throw new InvalidDataException("Login do usuário não é válido");

		if (string.IsNullOrEmpty(request.Password))
			throw new InvalidDataException("Senha do usuário não é válida");

		Guid userId = await _authenticationService.GetUserAsync(request);
		return userId;
	}
}
