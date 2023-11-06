using GhostBank.Application.DTOs;
using GhostBank.Application.Interface;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces;
using GhostBank.Domain.Models;
using GhostBank.Domain.Requests;
using InvalidDataException = GhostBank.Infrastructure.Middleware.Exceptions.Abstractions.InvalidDataException;

namespace GhostBank.Application.Services;

public class ApplicationUserService(IDomainUserService userService) : IApplicationUserService
{
	private readonly IDomainUserService _userService = userService;

	public async Task<UserDTO> GetByIdAsync(Guid id)
	{
		UserModel model = await _userService.GetByIdAsync(id);
		return new UserDTO(model);
	}

	public async Task<List<UserDTO>> GetAllAsync()
	{
		List<UserModel> result = await _userService.GetAllAsync();
		return result.Select(x => new UserDTO(x)).ToList();
	}

	public async Task<PaginatedListDTO<UserDTO>> GetAsync(Search<UserRequest> search)
	{
		PaginatedListModel<UserModel> result = await _userService.GetAsync(search);

		return new PaginatedListDTO<UserDTO>
		{
			Page = result.Page,
			Pages = result.Pages,
			Total = result.Total,
			Data = result.Data.Select(x => new UserDTO(x)).ToList()
		};
	}

	public async Task<PaginatedListDTO<UserDTO>> GetWithExcludedAsync(Search<UserRequest> search)
	{
		PaginatedListModel<UserModel> result = await _userService.GetWithExcludedAsync(search);

		return new PaginatedListDTO<UserDTO>
		{
			Page = result.Page,
			Pages = result.Pages,
			Total = result.Total,
			Data = result.Data.Select(x => new UserDTO(x)).ToList()
		};
	}

	public async Task CreateAsync(UserRequest request)
	{
		Validate(request);
		await _userService.CreateAsync(request);
	}

	public async Task UpdateAsync(UserRequest request)
	{
		Validate(request);
		await _userService.UpdateAsync(request);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _userService.DeleteAsync(id);
	}


	private static void Validate(UserRequest request)
	{
		if (string.IsNullOrEmpty(request.FirstName))
			throw new InvalidDataException("Primeiro nome não pode ser vazio");

		if (string.IsNullOrEmpty(request.LastName))
			throw new InvalidDataException("Sobrenome não pode ser vazio");

		if (string.IsNullOrEmpty(request.UserName))
			throw new InvalidDataException("Nome de usuário não pode ser vazio");

		if (string.IsNullOrEmpty(request.Email))
			throw new InvalidDataException("E-mail não pode ser vazio");

		if (string.IsNullOrEmpty(request.Password))
			throw new InvalidDataException("Senha não pode ser vazia");
	}
}
