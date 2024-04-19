using GhostBank.Application.DTOs;
using GhostBank.Application.Interface.Identity;
using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Helpers.Extensions;
using GhostBank.Domain.Interfaces.Identity;
using GhostBank.Domain.Models;
using GhostBank.Domain.Models.Identity;
using GhostBank.Domain.Requests.Identity;
using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System.Net;
using InvalidDataException = GhostBank.Domain.Exceptions.Abstractions.InvalidDataException;

namespace GhostBank.Application.Services.Identity;

public class ApplicationUserService(
	IHttpContextAccessor contextAccessor,
	IDomainUserService userService
) : IApplicationUserService
{
	private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
	private readonly IDomainUserService _userService = userService;

	private User User => _contextAccessor.HttpContext?.Items[nameof(User)] as User ?? throw new UnauthorizedAccessException();

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
		if (string.IsNullOrEmpty(request.Name))
			throw new InvalidDataException("Nome não pode ser vazio");

		if (!string.IsNullOrEmpty(request.CPF))
		{
			if (!Validator.IsCPF(request.CPF))
				throw new InvalidDataException("O CPF informado não é válido");
		}
		else
		{
			throw new InvalidDataException("CPF não pode ser vazio");
		}

		if (!string.IsNullOrEmpty(request.CNPJ) && !Validator.IsCNPJ(request.CNPJ))
			throw new InvalidDataException("O CNPJ informado não é válido");

		if (string.IsNullOrEmpty(request.UserName))
			throw new InvalidDataException("Nome de usuário não pode ser vazio");

		if (string.IsNullOrEmpty(request.Email))
			throw new InvalidDataException("E-mail não pode ser vazio");

		if (string.IsNullOrEmpty(request.Cellphone))
			throw new InvalidDataException("Celular não pode ser vazio");

		if (string.IsNullOrEmpty(request.Password))
			throw new InvalidDataException("Senha não pode ser vazia");

		if (!request.Role.IsValid())
			throw new InvalidDataException("O perfil informado não é válido");

		if (request.Address is not null)
		{
			if (string.IsNullOrEmpty(request.Address.Street))
				throw new InvalidDataException("O obrigatório informar a rua/logradouro");

			if (string.IsNullOrEmpty(request.Address.District))
				throw new InvalidDataException("É obrigatório informar o bairro");

			if (string.IsNullOrEmpty(request.Address.City))
				throw new InvalidDataException("É obrigatório informar a cidade");

			if (string.IsNullOrEmpty(request.Address.ZipCode))
				throw new InvalidDataException("É obrigatório informar 0 CEP");

			if (!request.Address.State.IsValid())
				throw new InvalidDataException("O estado informado é inválido");

			request.Address.ZipCode = request.Address.ZipCode.Remove(".").Remove("-");
		}
		else
		{
			throw new CannotProcessException("É obrigatório informar um endereço", HttpStatusCode.UnprocessableEntity);
		}
	}
}
