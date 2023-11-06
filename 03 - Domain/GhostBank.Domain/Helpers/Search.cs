using GhostBank.Infrastructure.Middleware.Attributes;

namespace GhostBank.Domain.Helpers;

public class Search<TRequest> where TRequest : class
{
    public int Page { get; set; }
    public int Quantity { get; set; }
    public TRequest? Filter { get; set; }

    public Search()
    {
		Type type = typeof(TRequest);

        if (type.GetCustomAttributes(typeof(RequestAttribute), false).Length == 0)
        {
            throw new InvalidOperationException(
                "Não é possível criar uma instância do tipo especificado.", 
                new InvalidOperationException("O tipo especificado, obrigatoriamente, ser um Request"));
        }

        Filter = type.GetConstructor(Type.EmptyTypes)?.Invoke(null) as TRequest;
    }
}
