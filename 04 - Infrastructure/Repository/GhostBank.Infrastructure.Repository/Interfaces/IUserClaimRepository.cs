using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IUserClaimRepository : IBaseRepository<UserClaim>
{
	Task UpdateAsync(Guid userId, params UserClaim[] claims);
}
