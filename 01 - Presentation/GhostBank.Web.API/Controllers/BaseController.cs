using GhostBank.Domain.Helpers;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.Controllers;

public abstract class BaseController<TRequest> : Controller where TRequest : class
{
	protected new User User 
	{
		get => HttpContext.Items[nameof(User)] as User ?? throw new UnauthorizedAccessException(); 
	}

	public virtual Task<IActionResult> GetById(Guid id) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> GetAll() => throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> Get([FromQuery] Search<TRequest> search) => 
		throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> GetWithExcluded([FromQuery] Search<TRequest> search) => 
		throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> Create([FromBody] TRequest request) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> Update([FromBody] TRequest request) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> Delete(Guid id) => throw new NotImplementedException("Recurso ainda não implementado");
}
