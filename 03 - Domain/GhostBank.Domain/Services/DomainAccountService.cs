using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Helpers.Extensions;
using GhostBank.Domain.Interfaces;
using GhostBank.Domain.Models;
using GhostBank.Domain.Requests;
using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using GhostBank.Infrastructure.Repository.Specifications.Contracts.Bank;

namespace GhostBank.Domain.Services;

public class DomainAccountService(IAccountRepository accountRepository) : IDomainAccountService
{
	private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));

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

	public async Task<PaginatedListModel<AccountModel>> GetAsync(Search<AccountRequest> search)
	{
		Specification<Account> specification = GetSpecification(search);
		PaginatedList<Account> accounts = await _accountRepository.GetAsync(search.Page, search.Quantity, specification);

		var result = new PaginatedListModel<AccountModel>
		{
			Page = accounts.Page,
			Pages = accounts.Pages,
			Total = accounts.Total,
			Data = accounts.Select(x => new AccountModel(x)).ToList()
		};

		return result;
	}

	public Task<PaginatedListModel<AccountModel>> GetWithExcludedAsync(Search<AccountRequest> search)
	{
		throw new NotImplementedException();
	}

	public Task CreateAsync(AccountRequest model)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(AccountRequest model)
	{
		throw new NotImplementedException();
	}

	private static Specification<Account> GetSpecification(Search<AccountRequest> search)
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
