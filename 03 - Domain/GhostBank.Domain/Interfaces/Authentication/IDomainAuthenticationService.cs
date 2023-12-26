using GhostBank.Domain.Helpers;
using GhostBank.Domain.Requests;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace GhostBank.Domain.Interfaces.Authentication;

public interface IDomainAuthenticationService
{
    Task<Guid> GetUserAsync(SignInRequest request);
    Task<string> AuthenticateAsync(SignInRequest request, HttpContext context);
}
