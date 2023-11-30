using System.Net;

namespace GhostBank.Domain.Exceptions.Abstractions;

public sealed class NotFoundException(string message) : BaseException(message, HttpStatusCode.NotFound)
{
}
