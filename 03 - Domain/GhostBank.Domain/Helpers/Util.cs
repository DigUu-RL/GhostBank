using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace GhostBank.Domain.Helpers;

public static class Util
{
	public static async ValueTask<string> CreateHashAsync(string value)
	{
		byte[] hash = await SHA512.Create().ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(value)));
		return Convert.ToBase64String(hash);
	}

	public static void ValidateJwtToken(string token, string jwtKey)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		byte[] key = Encoding.ASCII.GetBytes(jwtKey);

		var parameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false,
			ClockSkew = TimeSpan.Zero
		};

		tokenHandler.ValidateToken(token, parameters, out _);
	}
}
