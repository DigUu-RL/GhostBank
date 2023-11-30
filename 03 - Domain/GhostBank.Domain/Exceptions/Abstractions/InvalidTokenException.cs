using System.Net;

namespace GhostBank.Domain.Exceptions.Abstractions;

public class InvalidTokenException(string message) : BaseException(message, HttpStatusCode.Unauthorized)
{
}
