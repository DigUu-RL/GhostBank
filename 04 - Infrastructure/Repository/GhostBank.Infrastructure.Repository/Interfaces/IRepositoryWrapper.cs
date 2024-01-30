using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IRepositoryWrapper
{
	IBaseRepository<Account> Account { get; }
	IBaseRepository<User> User { get; }
	IBaseRepository<UserClaim> UserClaim { get; }
}
