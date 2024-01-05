namespace GhostBank.Domain.Helpers.Extensions;

public static class StringExtensions
{
	/// <summary>
	/// Try converts <see langword="string"/> as <see cref="Guid"/>
	/// </summary>
	/// <param name="s"></param>
	/// <returns>
	/// If the conversion is <see langword="true"/>, returns the corresponding <see cref="Guid"/>, otherwise <see cref="Guid.Empty"/>
	/// </returns>
	public static Guid ToGuidOrEmpty(this string? s)
	{
		return Guid.TryParse(s, out Guid result) ? result : Guid.Empty;
	}

	/// <summary>
	/// Remove the specifying term of <see langword="string"/>
	/// </summary>
	/// <param name="s"></param>
	/// <param name="term"></param>
	/// <returns>A new <see langword="string"/> without the specified term</returns>
	public static string Remove(this string s, string term)
	{
		return s.Replace(term, string.Empty);
	}
}
