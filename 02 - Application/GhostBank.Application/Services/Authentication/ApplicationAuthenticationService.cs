using GhostBank.Application.DTOs.Authentication;
using GhostBank.Application.Interface.Authentication;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Domain.Models.Authentication;
using GhostBank.Domain.Requests.Authentication;
using Microsoft.AspNetCore.Http;
using InvalidDataException = GhostBank.Domain.Exceptions.Abstractions.InvalidDataException;

namespace GhostBank.Application.Services.Authentication;

public class ApplicationAuthenticationService(IDomainAuthenticationService authenticationService) : IApplicationAuthenticationService
{
	private readonly IDomainAuthenticationService _authenticationService = authenticationService;

	public async Task<AccessTokenDTO> AuthenticateAsync(SignInRequest request, HttpContext context)
	{
		AccessTokenModel model = await _authenticationService.AuthenticateAsync(request, context);

		var accessToken = new AccessTokenDTO
		{
			ExpiresIn = model.ExpiresIn,
			Token = model.Token
		};

		return accessToken;
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
