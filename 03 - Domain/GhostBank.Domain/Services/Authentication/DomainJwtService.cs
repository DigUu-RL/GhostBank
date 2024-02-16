using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers.Extensions;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Domain.Models.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GhostBank.Domain.Services.Authentication;

public class DomainJwtService(IConfiguration configuration, IUserRepository userRepository) : IDomainJwtService
{
	private readonly IConfigurationSection _jwtSection = configuration.GetSection("JWT");
	private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	private readonly JwtSecurityTokenHandler _tokenHandler = new();

	public AccessTokenModel GenerateToken(User user)
	{
		byte[] key = Encoding.UTF8.GetBytes(_jwtSection.GetValue<string>("Key")!);

		string issuer = _jwtSection.GetValue<string>("Issuer")!;
		string audience = _jwtSection.GetValue<string>("Audience")!;
		string subject = _jwtSection.GetValue<string>("Subject")!;

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, subject),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString())
		};

		foreach (UserClaim claim in user.Claims)
			claims.Add(claim);

		var securityKey = new SymmetricSecurityKey(key);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		DateTime expires = DateTime.UtcNow.AddHours(2);

		var token = new JwtSecurityToken(
			issuer,
			audience,
			claims,
			expires: expires,
			signingCredentials: credentials);

		var accessToken = new AccessTokenModel
		{
			ExpiresIn = expires,
			Token = _tokenHandler.WriteToken(token)
		};

		return accessToken;
	}

	public async Task<User> ValidateTokenAsync(string token)
	{
		string type = token.Split(" ").First();
		string value = token.Split(" ").Last();

		if (!type.Equals("Bearer", StringComparison.CurrentCultureIgnoreCase))
			throw new InvalidTokenException("Tipo de token inválido");

		byte[] key = Encoding.UTF8.GetBytes(_jwtSection.GetValue<string>("Key")!);

		TokenValidationResult jwtToken = await _tokenHandler.ValidateTokenAsync(value, new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false,
			ClockSkew = TimeSpan.Zero
		});

		if (!jwtToken.IsValid)
			throw new InvalidTokenException("Token inválido");

		Guid userId = (jwtToken.Claims.Single(x => x.Key.Equals(nameof(User.Id))).Value as string).ToGuidOrEmpty();
		return await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Usuário não encontrado");
	}
}
