using Asp.Versioning;
using GhostBank.Application.DTOs;
using GhostBank.Application.DTOs.Bank;
using GhostBank.Application.Interface.Bank;
using GhostBank.Domain.Attributes;
using GhostBank.Domain.Requests;
using GhostBank.Domain.Requests.Bank;
using GhostBank.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.V1.Bank;

[Authorize]
[ApiController]
[ApiVersion(1)]
[Route("api/bank/[controller]/")]
public class AccountController(IApplicationAccountService accountService) : GhostBankController<AccountRequest>
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
	public override async Task<IActionResult> Get([FromQuery] SearchRequest<AccountRequest> search)
	{
		PaginatedListDTO<AccountDTO> accounts = await _accountService.GetAsync(search);
		return Ok(accounts);
	}

	[HttpGet("excluded")]
	public override async Task<IActionResult> GetWithExcluded([FromQuery] SearchRequest<AccountRequest> search)
	{
		PaginatedListDTO<AccountDTO> accounts = await _accountService.GetWithExcludedAsync(search);
		return Ok(accounts);
	}

	[HttpPost("create")]
	public override async Task<IActionResult> Create([FromBody] AccountRequest request)
	{
		return Ok("Not implemented");
	}

	[HttpPut("update")]
	public override async Task<IActionResult> Update([FromBody] AccountRequest request)
	{
		return Ok("Not implemented");
	}

	[HttpDelete("delete/{id}")]
	public override async Task<IActionResult> Delete(Guid id)
	{
		return Ok("Not implemented");
	}
}
