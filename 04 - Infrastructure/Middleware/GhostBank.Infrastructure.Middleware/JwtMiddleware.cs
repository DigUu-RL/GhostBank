using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace GhostBank.Infrastructure.Middleware;

public class JwtMiddleware(RequestDelegate next, IServiceProvider provider)
{
	private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
	private readonly IServiceProvider _provider = provider ?? throw new ArgumentNullException(nameof(provider));

	private readonly IDomainJwtService _jwtService = provider.CreateScope().ServiceProvider.GetRequiredService<IDomainJwtService>();

	public async Task InvokeAsync(HttpContext context)
	{
		string? token = context.Request.Headers[nameof(Authorization)].SingleOrDefault();

		if (!string.IsNullOrEmpty(token))
			await AttachUserAsync(context, token);

		await _next.Invoke(context);
	}

	private async Task AttachUserAsync(HttpContext context, string token)
	{
		User user = await _jwtService.ValidateTokenAsync(token);
		context.Items[nameof(User)] = user;
	}
}
