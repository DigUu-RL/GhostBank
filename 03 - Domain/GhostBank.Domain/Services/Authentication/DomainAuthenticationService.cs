using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Domain.Requests.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;
using GhostBank.Infrastructure.Repository.Specifications.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace GhostBank.Domain.Services.Authentication;

public class DomainAuthenticationService(IDomainJwtService jwtService, IUserRepository userRepository) : IDomainAuthenticationService
{
    private readonly IDomainJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<string> AuthenticateAsync(SignInRequest request, HttpContext context)
    {
        _userRepository.With(x => x.Include(user => user.Claims));

        User user = await _userRepository.GetByIdAsync(request.UserId) ??
            throw new NotFoundException("Usuário não encontrado");

        string token = _jwtService.GenerateToken(user);

        context.User = new ClaimsPrincipal(user);
        Thread.CurrentPrincipal = user;

        return token;
    }

    public async Task<Guid> GetUserAsync(SignInRequest request)
    {
        User? user = await _userRepository.Query(UserSpecification.ByEmail(request.Login!)).SingleOrDefaultAsync();
        user ??= await _userRepository.Query(UserSpecification.ByUserName(request.Login!)).SingleOrDefaultAsync();

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        string hash = await Util.CreateHashAsync(request.Password!);

        if (user.Password != hash)
            throw new CannotProcessException("As senhas não correspondem", HttpStatusCode.Forbidden);

        return user.Id;
    }
}
