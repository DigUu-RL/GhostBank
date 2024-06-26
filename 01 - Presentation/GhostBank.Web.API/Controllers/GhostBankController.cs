﻿using GhostBank.Domain.Requests;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.Controllers;

public abstract class GhostBankController<TRequest> : Controller where TRequest : class
{
	protected new User User => HttpContext.Items[nameof(User)] as User ?? throw new UnauthorizedAccessException();

	public virtual Task<IActionResult> GetById(Guid id) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> GetAll() => throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> Get([FromQuery] SearchRequest<TRequest> search) =>
		throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> GetWithExcluded([FromQuery] SearchRequest<TRequest> search) =>
		throw new NotImplementedException("Recurso ainda não implementado");

	public virtual Task<IActionResult> Create([FromBody] TRequest request) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> Update([FromBody] TRequest request) => throw new NotImplementedException("Recurso ainda não implementado");
	public virtual Task<IActionResult> Delete(Guid id) => throw new NotImplementedException("Recurso ainda não implementado");
}
