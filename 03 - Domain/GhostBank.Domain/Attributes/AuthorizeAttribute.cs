using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GhostBank.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		bool isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

		if (isAnonymous)
			return;

		if (context.HttpContext.Items[nameof(User)] is not User)
		{
			context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;

			context.Result = new JsonResult(new
			{
				statusCode = HttpStatusCode.Unauthorized,
				error = HttpStatusCode.Unauthorized.ToString(),
				message = "Não autenticado"
			});
		}
	}
}
