using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces.Identity;

public interface IUserClaimRepository : IBaseRepository<UserClaim>
{
    Task UpdateAsync(Guid userId, params UserClaim[] claims);
}
