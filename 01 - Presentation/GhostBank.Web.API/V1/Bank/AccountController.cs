using Asp.Versioning;
using GhostBank.Application.DTOs;
using GhostBank.Application.DTOs.Bank;
using GhostBank.Application.Interface.Bank;
using GhostBank.Domain.Attributes;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Requests;
using GhostBank.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.V1.Bank;

[Authorize]
[ApiController]
[ApiVersion(1)]
[Route("api/[controller]/")]
public class AccountController(IApplicationAccountService accountService) : BaseController<AccountRequest>
{
	private readonly IApplicationAccountService _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

	[HttpGet("{id}")]
	public override async Task<IActionResult> GetById(Guid id)
	{
		AccountDTO account = await _accountService.GetByIdAsync(id);
		return Ok(account);
	}

	[HttpGet("all")]
	public override async Task<IActionResult> GetAll()
	{
		List<AccountDTO> accounts = await _accountService.GetAllAsync();
		return Ok(accounts);
	}

	[HttpGet("list")]
	public override async Task<IActionResult> Get([FromQuery] Search<AccountRequest> search)
	{
		PaginatedListDTO<AccountDTO> accounts = await _accountService.GetAsync(search);
		return Ok(accounts);
	}

	[HttpGet("excluded")]
	public override async Task<IActionResult> GetWithExcluded([FromQuery] Search<AccountRequest> search)
	{
		PaginatedListDTO<AccountDTO> accounts = await _accountService.GetWithExcludedAsync(search);
		return Ok(accounts);
	}
}
