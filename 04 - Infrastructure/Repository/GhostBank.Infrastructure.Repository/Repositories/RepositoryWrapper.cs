using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class RepositoryWrapper(IUserRepository userRepository, IUserClaimRepository userClaimRepository) : IRepositoryWrapper
{
	public IBaseRepository<User> User { get; } = userRepository;
	public IBaseRepository<UserClaim> UserClaim { get; } = userClaimRepository;
}
