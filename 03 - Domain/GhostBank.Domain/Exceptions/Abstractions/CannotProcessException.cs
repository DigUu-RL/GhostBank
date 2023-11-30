using System.Net;

namespace GhostBank.Domain.Exceptions.Abstractions;

public class CannotProcessException(string message, HttpStatusCode statusCode) : BaseException(message, statusCode)
{
}
