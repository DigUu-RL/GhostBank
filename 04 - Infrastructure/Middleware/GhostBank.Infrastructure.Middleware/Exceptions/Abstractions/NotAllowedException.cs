using System.Net;

namespace GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;

public sealed class NotAllowedException(string message) : BaseException(message, HttpStatusCode.Forbidden)
{
}
