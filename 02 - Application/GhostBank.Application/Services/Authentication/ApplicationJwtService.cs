using GhostBank.Application.DTOs;
using GhostBank.Application.Interface.Authentication;
using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Application.Services.Authentication;

public class ApplicationJwtService(IDomainJwtService jwtService) : IApplicationJwtService
{
	private readonly IDomainJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

	public async Task<UserDTO> ValidateTokenAsync(string? token)
	{
		if (string.IsNullOrEmpty(token))
			throw new InvalidTokenException("Token inválido");

		User user = await _jwtService.ValidateTokenAsync(token);

		return new UserDTO
		{
			Id = user.Id,
			Name = user.Name,
			UserName = user.UserName,
			Email = user.Email,
			Role = user.Role
		};
	}
}
