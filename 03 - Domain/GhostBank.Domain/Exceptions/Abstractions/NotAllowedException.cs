using System.Net;

namespace GhostBank.Domain.Exceptions.Abstractions;

public sealed class NotAllowedException(string message) : BaseException(message, HttpStatusCode.Forbidden)
{
}
