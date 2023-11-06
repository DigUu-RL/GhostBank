using GhostBank.Domain.Helpers;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.Controllers;

public abstract class BaseController<TRequest> : Controller where TRequest : class
{
	protected new User? User { get; set; }

	public abstract Task<IActionResult> GetById(Guid id);
	public abstract Task<IActionResult> GetAll();
	public abstract Task<IActionResult> Get([FromQuery] Search<TRequest> search);
	public abstract Task<IActionResult> GetWithExcluded([FromQuery] Search<TRequest> search);
	public abstract Task<IActionResult> Create([FromBody] TRequest request);
	public abstract Task<IActionResult> Update([FromBody] TRequest request);
	public abstract Task<IActionResult> Delete(Guid id);
}
