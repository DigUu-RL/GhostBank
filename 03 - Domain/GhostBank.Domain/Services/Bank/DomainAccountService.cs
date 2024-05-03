using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers.Extensions;
using GhostBank.Domain.Interfaces.Bank;
using GhostBank.Domain.Models;
using GhostBank.Domain.Models.Bank;
using GhostBank.Domain.Requests;
using GhostBank.Domain.Requests.Bank;
using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Interfaces.Bank;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using GhostBank.Infrastructure.Repository.Specifications.Contracts.Bank;

namespace GhostBank.Domain.Services.Bank;

public class DomainAccountService(IAccountRepository accountRepository) : IDomainAccountService
{
	private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));

	public IRepositoryWrapper Repository => throw new NotImplementedException();

	public async Task<AccountModel> GetByIdAsync(Guid id)
	{
		Account account = await _accountRepository.GetByIdAsync(id) ?? throw new NotFoundException("Conta não encontrada");

		var model = new AccountModel(account);
		return model;
	}

	public async Task<List<AccountModel>> GetAllAsync()
	{
		List<Account> accounts = await _accountRepository.GetAllAsync();

		List<AccountModel> result = accounts.Select(x => new AccountModel(x)).ToList();
		return result;
	}

	public async Task<PaginatedListModel<AccountModel>> GetAsync(SearchRequest<AccountRequest> request)
	{
		Specification<Account> specification = GetSpecification(request);

		var search = new Search<Account>
		{
			Page = request.Page,
			Quantity = request.Quantity,
			Specification = specification
		};

		PaginatedList<Account> accounts = await _accountRepository.GetAsync(search);

		var result = new PaginatedListModel<AccountModel>
		{
			Page = accounts.Page,
			Pages = accounts.Pages,
			Total = accounts.Total,
			Data = accounts.Select(x => new AccountModel(x)).ToList()
		};

		return result;
	}

	public async Task<PaginatedListModel<AccountModel>> GetWithExcludedAsync(SearchRequest<AccountRequest> request)
	{
		Specification<Account> specification = GetSpecification(request);

		var search = new Search<Account>
		{
			Page = request.Page,
			Quantity = request.Quantity,
			Specification = specification
		};

		PaginatedList<Account> accounts = await _accountRepository.GetWithExcludedAsync(search);

		var result = new PaginatedListModel<AccountModel>
		{
			Page = accounts.Page,
			Pages = accounts.Pages,
			Total = accounts.Total,
			Data = accounts.Select(x => new AccountModel(x)).ToList()
		};

		return result;
	}

	public async Task CreateAsync(AccountRequest request)
	{
		var account = new Account
		{
			Agency = request.Agency!,
			Number = request.Number!,
			Type = request.Type
		};

		await _accountRepository.CreateAsync(account);
	}

	public async Task UpdateAsync(AccountRequest request)
	{
		Account account = await _accountRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Conta não encontrada");

		account.Agency = request.Agency!;
		account.Number = request.Number!;
		account.Type = request.Type;

		await _accountRepository.UpdateAsync(account);
	}

	public async Task DeleteAsync(Guid id)
	{
		Account account = await _accountRepository.GetByIdAsync(id) ?? throw new NotFoundException("Conta não encontrada");
		await _accountRepository.DeleteAsync(account);
	}

	private static Specification<Account> GetSpecification(SearchRequest<AccountRequest> search)
	{
		Specification<Account> specification = new TrueSpecification<Account>();

		if (search.Filter is not null)
		{
			if (!search.Filter.Id.Equals(Guid.Empty))
				specification &= AccountSpecification.ById(search.Filter.Id);

			if (!string.IsNullOrEmpty(search.Filter.Agency))
				specification &= AccountSpecification.ByAgency(search.Filter.Agency);

			if (!string.IsNullOrEmpty(search.Filter.Number))
				specification &= AccountSpecification.ByNumber(search.Filter.Number);

			if (search.Filter.Balance > 0)
				specification &= AccountSpecification.ByBalance(search.Filter.Balance);

			if (search.Filter.Type.IsValid())
				specification &= AccountSpecification.ByType(search.Filter.Type);
		}

		return specification;
	}
}
