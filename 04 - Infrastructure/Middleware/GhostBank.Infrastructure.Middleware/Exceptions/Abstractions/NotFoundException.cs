using System.Net;

namespace GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;

public sealed class NotFoundException(string message) : BaseException(message, HttpStatusCode.NotFound)
{
}
