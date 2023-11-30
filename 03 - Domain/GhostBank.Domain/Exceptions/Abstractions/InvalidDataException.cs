using System.Net;

namespace GhostBank.Domain.Exceptions.Abstractions;

public class InvalidDataException(string message) : BaseException(message, HttpStatusCode.BadRequest)
{
}
