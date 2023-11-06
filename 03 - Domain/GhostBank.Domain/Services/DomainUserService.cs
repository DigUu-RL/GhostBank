using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces;
using GhostBank.Domain.Models;
using GhostBank.Domain.Requests;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Middleware.Exceptions.Abstractions;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using GhostBank.Infrastructure.Repository.Specifications.Contracts;

namespace GhostBank.Domain.Services;

public class DomainUserService(IUserRepository userRepository) : IDomainUserService
{
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<UserModel> GetByIdAsync(Guid id)
	{
		User? user = await _userRepository.GetByIdAsync(id);
		return user is not null ? new UserModel(user) : throw new NotFoundException("Usuário não encontrado");
	}

	public async Task<List<UserModel>> GetAllAsync()
	{
		List<User> users = await _userRepository.GetAllAsync();
		return users.Select(x => new UserModel(x)).ToList();
	}

	public async Task<PaginatedListModel<UserModel>> GetAsync(Search<UserRequest> search)
	{
		Specification<User> specification = GetSpecification(search);
		PaginatedList<User> result = await _userRepository.GetAsync(search.Page, search.Quantity, specification);

		return new PaginatedListModel<UserModel>
		{
			Page = result.Page,
			Pages = result.Pages,
			Total = result.Total,
			Data = result.Select(x => new UserModel(x)).ToList()
		};
	}

	public async Task<PaginatedListModel<UserModel>> GetWithExcludedAsync(Search<UserRequest> search)
	{
		Specification<User> specification = GetSpecification(search);
		PaginatedList<User> result = await _userRepository.GetWithExcludedAsync(search.Page, search.Quantity, specification);

		return new PaginatedListModel<UserModel>
		{
			Page = result.Page,
			Pages = result.Pages,
			Total = result.Total,
			Data = result.Select(x => new UserModel(x)).ToList()
		};
	}

	public async Task CreateAsync(UserRequest model)
	{
		var user = new User
		{
			FirstName = model.FirstName!,
			LastName = model.LastName!,
			Email = model.Email!,
			Roles = model.Roles
		};

		string combine = model.Password + model.Email;
		user.Password = await Util.CreateHashAsync(combine);

		await _userRepository.CreateAsync(user);
		await _userRepository.CommitAsync();
	}

	public async Task UpdateAsync(UserRequest model)
	{
		User? user = await _userRepository.GetByIdAsync(model.Id) ?? throw new NotFoundException("Usuário não encontrado");

		user.FirstName = model.FirstName!;
		user.LastName = model.LastName!;
		user.Email = model.Email!;
		user.Roles = model.Roles;

		await _userRepository.UpdateAsync(user);
		await _userRepository.CommitAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		User? user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado");

		await _userRepository.DeleteAsync(user);
		await _userRepository.CommitAsync();
	}

	private static Specification<User> GetSpecification(Search<UserRequest> search)
	{
		Specification<User> specification = new TrueSpecification<User>();

		if (search.Filter is not null)
		{
			if (search.Filter.Id.HasValue)
				specification &= UserSpecification.ById(search.Filter.Id.Value);

			if (!string.IsNullOrEmpty(search.Filter.FirstName))
				specification &= UserSpecification.ByFirstName(search.Filter.FirstName);

			if (!string.IsNullOrEmpty(search.Filter.LastName))
				specification &= UserSpecification.ByLastName(search.Filter.LastName);

			if (!string.IsNullOrEmpty(search.Filter.Email))
				specification &= UserSpecification.ByEmail(search.Filter.Email);
		}

		return specification;
	}
}
