using GhostBank.Infrastructure.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace GhostBank.Infrastructure.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next.Invoke(context);
		}
		catch (BaseException ex)
		{
			await HandleExceptionAsync(context, ex, ex.StatusCode);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode = default)
	{
		if (ex is not BaseException)
			statusCode = HttpStatusCode.InternalServerError;

		var response = new
		{
			statusCode,
			errorName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(statusCode.ToString()),
			errorMessage = ex.Message,
			innerErrorMessage = ex.InnerException?.Message
		};

		string json = JsonConvert.SerializeObject(response);

		context.Response.ContentType = "application/json";
		context.Response.ContentLength = (int) statusCode;

		await context.Response.WriteAsync(json);
	}
}
