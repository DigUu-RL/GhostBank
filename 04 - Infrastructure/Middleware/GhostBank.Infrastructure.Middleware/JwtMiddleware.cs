using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace GhostBank.Infrastructure.Middleware;

public class JwtMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

	public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
	{
		IDomainJwtService jwtService = provider.CreateScope().ServiceProvider.GetRequiredService<IDomainJwtService>();

		string? token = context.Request.Headers[nameof(Authorization)].SingleOrDefault();

		if (!string.IsNullOrEmpty(token))
			await AttachUserAsync(context, jwtService, token);

		await _next.Invoke(context);
	}

	private async Task AttachUserAsync(HttpContext context, IDomainJwtService jwtService, string token)
	{
		User user = await jwtService.ValidateTokenAsync(token);
		context.Items[nameof(User)] = user;
	}
}
