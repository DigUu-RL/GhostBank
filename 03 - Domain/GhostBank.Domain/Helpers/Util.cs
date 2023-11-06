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
}
