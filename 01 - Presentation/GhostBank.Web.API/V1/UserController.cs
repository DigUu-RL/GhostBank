using GhostBank.Application.DTOs;
using GhostBank.Application.Interface;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Requests;
using GhostBank.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.V1;

public class UserController(IApplicationUserService userService) : BaseController<UserRequest>
{
	private readonly IApplicationUserService _userService = userService;

	[HttpGet("/{id}")]
	public override async Task<IActionResult> GetById(Guid id)
	{
		UserDTO user = await _userService.GetByIdAsync(id);
		return Ok(user);
	}

	[HttpGet("/all")]
	public override Task<IActionResult> GetAll()
	{
		throw new NotImplementedException();
	}

	[HttpGet("/list")]
	public override Task<IActionResult> Get([FromQuery] Search<UserRequest> search)
	{
		throw new NotImplementedException();
	}

	[HttpGet("/excluded")]
	public override Task<IActionResult> GetWithExcluded([FromQuery] Search<UserRequest> search)
	{
		throw new NotImplementedException();
	}

	[HttpPost("/create")]
	public override Task<IActionResult> Create([FromBody] UserRequest request)
	{
		throw new NotImplementedException();
	}


	[HttpPut("/update")]
	public override Task<IActionResult> Update([FromBody] UserRequest request)
	{
		throw new NotImplementedException();
	}

	[HttpDelete("/delete")]
	public override Task<IActionResult> Delete(Guid id)
	{
		throw new NotImplementedException();
	}
}
