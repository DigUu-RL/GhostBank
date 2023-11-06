using System.Net;

namespace GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;

public class InvalidDataException(string message) : BaseException(message, HttpStatusCode.BadRequest)
{
}
