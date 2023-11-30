using System.Net;
using System.Reflection;
using GhostBank.Domain.Exceptions.Abstractions;
using InvalidDataException = GhostBank.Domain.Exceptions.Abstractions.InvalidDataException;
namespace GhostBank.Domain.Helpers.Extensions;

public static class GenericExtensions
{
	/// <summary>
	/// Verifica se o valor atual está entre os valores especificados
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <param name="left">Valor que será comparado à esquerda (maior ou igual)</param>
	/// <param name="right">Valor que será comparado à direita (menor ou igual)</param>
	/// <returns><see langword="true"/> caso o valor esteja entre os valores especificados, caso contrário, <see langword="false"/></returns>
	public static bool Between<T>(this IComparable<T> value, T left, T right) where T : IComparable<T>
	{
		return value.CompareTo(left) >= 0 && value.CompareTo(right) <= 0;
	}

	/// <summary>
	/// Cria uma cópia do objeto atual em uma nova instância
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns>Uma nova instância do objeto atual com os mesmo valores</returns>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static T Clone<T>(this T? value) where T : class
	{
		if (value is null)
			throw new ArgumentNullException(nameof(value), "Unable to create a copy of a null object");

		Type destinationType = typeof(T);
		PropertyInfo[] destinationProperties = destinationType.GetProperties();

		ConstructorInfo destinationConstructor = destinationType.GetConstructor(Type.EmptyTypes) ??
			throw new CannotProcessException(
				"Unable to create an instance for the specified object. Your class does not have a default constructor with no arguments",
				HttpStatusCode.BadRequest
			);

		T destination = destinationConstructor.Invoke(null) as T ??
				throw new CannotProcessException("Could not create an instance for the specified object type", HttpStatusCode.BadRequest);

		PropertyInfo[] sourceProperties = value.GetType().GetProperties();

		foreach (PropertyInfo sourceProperty in sourceProperties)
		{
			PropertyInfo? destinationProperty = destinationProperties.SingleOrDefault(x => x.Name.Equals(sourceProperty.Name));

			if (destinationProperty is null || !destinationProperty.CanWrite)
				continue;

			object? propertyValue = destinationProperty.GetValue(value);
			destinationProperty.SetValue(destination, propertyValue);
		}

		return destination;
	}

	/// <summary>
	/// Cria uma cópia do objeto atual em uma instância diferente da instância de origem
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <param name="destination"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static void CloneTo<T>(this T? value, ref T? destination) where T : class
	{
		if (value is null)
			throw new ArgumentNullException(nameof(value), "Unable to create a copy from a null object");

		if (destination is null)
			throw new ArgumentNullException(nameof(destination), "Unable to create a copy to a null object");

		if (ReferenceEquals(value, destination))
			throw new CannotProcessException("The destination object contains the same instance as the source object", HttpStatusCode.BadRequest);

		PropertyInfo[] sourceProperties = value.GetType().GetProperties();
		PropertyInfo[] destinationProperties = destination.GetType().GetProperties();

		foreach (PropertyInfo sourceProperty in sourceProperties)
		{
			PropertyInfo? destinationProperty = destinationProperties.SingleOrDefault(x => x.Name.Equals(sourceProperty.Name));

			if (destinationProperty is null || !destinationProperty.CanWrite)
				continue;

			object? propertyValue = destinationProperty.GetValue(value);
			destinationProperty.SetValue(destination, propertyValue);
		}
	}
}
