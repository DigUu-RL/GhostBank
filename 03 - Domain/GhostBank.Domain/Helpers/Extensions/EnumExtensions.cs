namespace GhostBank.Domain.Helpers.Extensions;

public static class EnumExtensions
{
	/// <summary>
	/// Verifica se o valor do <see cref="Enum"/> atual é válido
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <param name="enum"></param>
	/// <returns></returns>
	public static bool IsValid<TEnum>(this TEnum @enum) where TEnum : Enum
	{
		Type type = @enum.GetType();
		string name = @enum.ToString();

		string[] names = Enum.GetNames(type);
		return names.Contains(name);
	}
}
