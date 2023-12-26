using GhostBank.Application.Interface.Authentication;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Domain.Requests;
using Microsoft.AspNetCore.Http;
using InvalidDataException = GhostBank.Domain.Exceptions.Abstractions.InvalidDataException;

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
