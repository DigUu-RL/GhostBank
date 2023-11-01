using System.Net;

namespace GhostBank.Infrastructure.Middleware.Exceptions;

public class BaseException(string message, HttpStatusCode statusCode) : Exception(message)
{
	public HttpStatusCode StatusCode { get; } = statusCode;
}
