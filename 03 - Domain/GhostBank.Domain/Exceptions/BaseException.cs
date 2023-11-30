using System.Net;

namespace GhostBank.Domain.Exceptions;

public class BaseException(string message, HttpStatusCode statusCode) : Exception(message)
{
	public HttpStatusCode StatusCode { get; } = statusCode;
}
