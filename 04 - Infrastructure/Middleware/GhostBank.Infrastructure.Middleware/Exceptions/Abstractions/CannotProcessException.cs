using System.Net;

namespace GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;

public class CannotProcessException(string message, HttpStatusCode statusCode) : BaseException(message, statusCode)
{
}
