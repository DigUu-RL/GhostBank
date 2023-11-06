namespace GhostBank.Domain.Helpers.Extensions;

public static class StringExtensions
{
	public static string Remove(this string s, string term)
	{
		return s.Remove(s.IndexOf(term), s.Length);
	}
}
