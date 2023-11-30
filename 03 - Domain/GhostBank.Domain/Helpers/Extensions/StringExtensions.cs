namespace GhostBank.Domain.Helpers.Extensions;

public static class StringExtensions
{
	public static Guid ToGuidOrEmpty(this string? s)
	{
		return Guid.TryParse(s, out Guid result) ? result : Guid.Empty;
	}

	public static string Remove(this string s, string term)
	{
		return s.Remove(s.IndexOf(term), s.Length);
	}
}
