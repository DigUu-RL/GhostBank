using System.Security.Cryptography;
using System.Text;

namespace GhostBank.Domain.Helpers;

public static class Util
{
	public static async ValueTask<string> CreateHashAsync(string value)
	{
		byte[] buffer = Encoding.UTF8.GetBytes(value);
		var stream = new MemoryStream(buffer);

		SHA512 hasher = SHA512.Create();
		byte[] bytes = await hasher.ComputeHashAsync(stream);

		string hash = Convert.ToBase64String(bytes);
		return hash;
	}
}
