using GhostBank.Domain.Interfaces;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GhostBank.Domain.Services;

public class DomainJwtService(IConfiguration configuration) : IDomainJwtService
{
	private readonly IConfigurationSection _jwtSection = configuration.GetSection("JWT");
	private readonly JwtSecurityTokenHandler _tokenHandler = new();

	public string GenerateToken(User user)
	{
		byte[] key = Encoding.UTF8.GetBytes(_jwtSection.GetValue<string>("Key")!);

		string issuer = _jwtSection.GetValue<string>("Issuer")!;
		string audience = _jwtSection.GetValue<string>("Audience")!;
		string subject = _jwtSection.GetValue<string>("Subject")!;

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, subject),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
		};

		foreach (UserClaim claim in user.Claims)
			claims.Add(new Claim(claim.Type, claim.Value));

		var securityKey = new SymmetricSecurityKey(key);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer,
			audience,
			claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: credentials);

		return _tokenHandler.WriteToken(token);
	}

	public void ValidateToken(string token)
	{
		byte[] key = Encoding.ASCII.GetBytes(_jwtSection.GetValue<string>("Key")!);

		_tokenHandler.ValidateToken(token, new TokenValidationParameters 
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false,
			ClockSkew = TimeSpan.Zero
		}, out SecurityToken validatedToken);

		_ = (JwtSecurityToken) validatedToken;
	}
}
