using GhostBank.Domain.Exceptions.Abstractions;
using GhostBank.Domain.Helpers;
using GhostBank.Domain.Interfaces.Identity;
using GhostBank.Domain.Models;
using GhostBank.Domain.Models.Identity;
using GhostBank.Domain.Requests.Identity;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using GhostBank.Infrastructure.Repository.Specifications.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GhostBank.Domain.Services.Identity;

public class DomainUserService(
	IHttpContextAccessor contextAccessor,
	IUserRepository userRepository,
	IUserClaimRepository userClaimRepository
) : IDomainUserService
{
	private readonly IHttpContextAccessor _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
	private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

	private readonly IUserClaimRepository _userClaimRepository =
		userClaimRepository ?? throw new ArgumentNullException(nameof(userClaimRepository));

	private User User => _contextAccessor.HttpContext?.Items[nameof(User)] as User ?? throw new UnauthorizedAccessException();

	public async Task<UserModel> GetByIdAsync(Guid id)
	{
		User user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado");

		var model = new UserModel(user);
		return model;
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

	public async Task CreateAsync(UserRequest request)
	{
		var user = new User
		{
			Name = request.Name!,
			CPF = request.CPF!,
			CNPJ = request.CNPJ,
			UserName = request.UserName!,
			Email = request.Email!,
			Cellphone = request.Cellphone!,
			Role = request.Role,
			Password = await Util.CreateHashAsync(request.Password!),
			Address = new Address
			{
				Street = request.Address!.Street!,
				Number = request.Address!.Number,
				District = request.Address!.District!,
				City = request.Address!.City!,
				ZipCode = request.Address!.ZipCode!,
				State = request.Address!.State,
				CreatedOn = DateTime.UtcNow,
				LastUpdate = DateTime.UtcNow
			}
		};

		await _userRepository.CreateAsync(user);
		await _userRepository.CommitAsync();

		var claims = new List<UserClaim>
		{
			new(nameof(User.Id), user.Id.ToString()),
			new(nameof(User.UserName), user.UserName),
			new(nameof(User.Email), user.Email),
			new(nameof(User.Role), user.Role.ToString())
		};

		claims.ForEach(x => x.UserId = user.Id);

		await _userClaimRepository.CreateAsync([.. claims]);
		await _userClaimRepository.CommitAsync();

		await _userRepository.GrantDataBaseAccess(user);
	}

	public async Task UpdateAsync(UserRequest request)
	{
		_userRepository.With(x => x.Claims);

		User user = await _userRepository.GetByIdAsync(request.Id.GetValueOrDefault()) ?? throw new NotFoundException("Usuário não encontrado");

		user.Name = request.Name!;
		user.UserName = request.UserName!;
		user.Email = request.Email!;
		user.Cellphone = request.Cellphone!;
		user.Role = request.Role;

		await _userRepository.UpdateAsync(user);
		await _userRepository.CommitAsync();

		var claims = new List<UserClaim>
		{
			new(nameof(User.Id), user.Id.ToString()),
			new(nameof(User.UserName), user.UserName),
			new(nameof(User.Email), user.Email),
			new(nameof(User.Role), user.Role.ToString())
		};

		await _userClaimRepository.UpdateAsync(user.Id, [.. claims]);
		await _userClaimRepository.CommitAsync();

		await _userRepository.GrantDataBaseAccess(user);
	}

	public async Task DeleteAsync(Guid id)
	{
		User user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado");

		try
		{
			await _userRepository.DeleteAsync(user);
			await _userRepository.CommitAsync();
		}
		catch (Exception)
		{
			user.Excluded = true;

			await _userRepository.UpdateAsync(user);
			await _userRepository.CommitAsync();
		}
	}

	private static Specification<User> GetSpecification(Search<UserRequest> search)
	{
		Specification<User> specification = new TrueSpecification<User>();

		if (search.Filter is not null)
		{
			if (search.Filter.Id.HasValue)
				specification &= UserSpecification.ById(search.Filter.Id.Value);

			if (!string.IsNullOrEmpty(search.Filter.Name))
				specification &= UserSpecification.ByName(search.Filter.Name);

			if (!string.IsNullOrEmpty(search.Filter.CPF))
				specification &= UserSpecification.ByCPF(search.Filter.CPF);

			if (!string.IsNullOrEmpty(search.Filter.CNPJ))
				specification &= UserSpecification.ByCNPJ(search.Filter.CNPJ);

			if (!string.IsNullOrEmpty(search.Filter.UserName))
				specification &= UserSpecification.ByUserName(search.Filter.UserName);

			if (!string.IsNullOrEmpty(search.Filter.Email))
				specification &= UserSpecification.ByEmail(search.Filter.Email);

			if (!string.IsNullOrEmpty(search.Filter.Cellphone))
				specification &= UserSpecification.ByCellphone(search.Filter.Cellphone);
		}

		return specification;
	}
}
