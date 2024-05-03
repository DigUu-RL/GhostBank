using GhostBank.Domain.Attributes;

namespace GhostBank.Domain.Requests;

public class SearchRequest<TRequest> where TRequest : class
{
	public int Page { get; set; }
	public int Quantity { get; set; }
	public TRequest? Filter { get; set; }

	public SearchRequest()
	{
		Type type = typeof(TRequest);

		if (type.GetCustomAttributes(typeof(RequestAttribute), false).Length == 0)
		{
			throw new InvalidOperationException(
				"Não é possível criar uma instância do tipo especificado.",
				new InvalidOperationException("O tipo especificado precisa ser, obrigatoriamente, ser um Request"));
		}

		Filter = type.GetConstructor(Type.EmptyTypes)?.Invoke(null) as TRequest;
	}
}
