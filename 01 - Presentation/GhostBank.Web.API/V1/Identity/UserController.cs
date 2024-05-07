using Asp.Versioning;
using GhostBank.Application.DTOs;
using GhostBank.Application.Interface.Identity;
using GhostBank.Domain.Requests;
using GhostBank.Domain.Requests.Identity;
using GhostBank.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = GhostBank.Domain.Attributes.AuthorizeAttribute;

namespace GhostBank.Web.API.V1.Identity;

[Authorize]
[ApiController]
[ApiVersion(1)]
[Route("api/identity/[controller]/")]
public class UserController(IApplicationUserService userService) : GhostBankController<UserRequest>
{
	private readonly IApplicationUserService _userService = userService;

	[HttpGet("{id}")]
	public override async Task<IActionResult> GetById(Guid id)
	{
		UserDTO user = await _userService.GetByIdAsync(id);
		return Ok(user);
	}

	[HttpGet("all")]
	public override async Task<IActionResult> GetAll()
	{
		List<UserDTO> users = await _userService.GetAllAsync();
		return Ok(users);
	}

	[HttpGet("list")]
	public override async Task<IActionResult> Get([FromQuery] SearchRequest<UserRequest> search)
	{
		PaginatedListDTO<UserDTO> users = await _userService.GetAsync(search);
		return Ok(users);
	}

	[HttpGet("excluded")]
	public override async Task<IActionResult> GetWithExcluded([FromQuery] SearchRequest<UserRequest> search)
	{
		PaginatedListDTO<UserDTO> users = await _userService.GetWithExcludedAsync(search);
		return Ok(users);
	}

	[HttpPost("create")]
	public override async Task<IActionResult> Create([FromBody] UserRequest request)
	{
		await _userService.CreateAsync(request);
		return Created();
	}

	[HttpPut("update")]
	public override async Task<IActionResult> Update([FromBody] UserRequest request)
	{
		await _userService.UpdateAsync(request);
		return NoContent();
	}

	[HttpDelete("delete/{id}")]
	public override async Task<IActionResult> Delete(Guid id)
	{
		await _userService.DeleteAsync(id);
		return NoContent();
	}
}
