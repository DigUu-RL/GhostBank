using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Interfaces.Bank;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class RepositoryWrapper(
	IAccountRepository accountRepository,
	IUserRepository userRepository,
	IUserClaimRepository userClaimRepository
) : IRepositoryWrapper
{
	public IBaseRepository<Account> Account { get; } = accountRepository;
	public IBaseRepository<User> User { get; } = userRepository;
	public IBaseRepository<UserClaim> UserClaim { get; } = userClaimRepository;
}
