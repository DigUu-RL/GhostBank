using GhostBank.Application.DTOs;
using GhostBank.Application.DTOs.Bank;
using GhostBank.Application.Interface.Bank;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces.Bank;
using GhostBank.Domain.Models;
using GhostBank.Domain.Models.Bank;
using GhostBank.Domain.Requests;

namespace GhostBank.Application.Services.Bank;

public class ApplicationAccountService(IDomainAccountService accountService) : IApplicationAccountService
{
	private readonly IDomainAccountService _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

	public async Task<AccountDTO> GetByIdAsync(Guid id)
	{
		AccountModel model = await _accountService.GetByIdAsync(id);

		var account = new AccountDTO(model);
		return account;
	}

	public async Task<List<AccountDTO>> GetAllAsync()
	{
		List<AccountModel> accounts = await _accountService.GetAllAsync();
		return accounts.Select(x => new AccountDTO(x)).ToList();
	}

	public async Task<PaginatedListDTO<AccountDTO>> GetAsync(Search<AccountRequest> search)
	{
		PaginatedListModel<AccountModel> accounts = await _accountService.GetAsync(search);

		var result = new PaginatedListDTO<AccountDTO>
		{
			Page = accounts.Page,
			Pages = accounts.Pages,
			Total = accounts.Total,
			Data = accounts.Data.Select(x => new AccountDTO(x)).ToList()
		};

		return result;
	}

	public async Task<PaginatedListDTO<AccountDTO>> GetWithExcludedAsync(Search<AccountRequest> search)
	{
		PaginatedListModel<AccountModel> accounts = await _accountService.GetWithExcludedAsync(search);

		var result = new PaginatedListDTO<AccountDTO>
		{
			Page = accounts.Page,
			Pages = accounts.Pages,
			Total = accounts.Total,
			Data = accounts.Data.Select(x => new AccountDTO(x)).ToList()
		};

		return result;
	}

	public async Task CreateAsync(AccountRequest request)
	{
		Validate(request);
		await _accountService.CreateAsync(request);
	}

	public async Task UpdateAsync(AccountRequest request)
	{
		Validate(request);
		await _accountService.UpdateAsync(request);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _accountService.DeleteAsync(id);
	}

	private static void Validate(AccountRequest request)
	{
		if (string.IsNullOrEmpty(request.Agency))
			throw new InvalidDataException("Agência é obrigatória");

		if (string.IsNullOrEmpty(request.Number))
			throw new InvalidDataException("Número da conta é obrigatório");
	}
}
