using GhostBank.Domain.Exceptions;
using GhostBank.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
		catch (ArgumentNullException ex)
		{
			await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
		}
		catch (NullReferenceException ex)
		{
			await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
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

		var response = new ErrorModel
		{
			StatusCode = (int) statusCode,
			Error = statusCode.ToString(),
			Message = ex.Message,
			Inner = ex.InnerException?.Message
		};

		string json = JsonConvert.SerializeObject(response);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int) statusCode;

		await context.Response.WriteAsync(json);
	}
}
