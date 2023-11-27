using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class UserClaimRepository(GhostBankContext context) : BaseRepository<UserClaim>(context), IUserClaimRepository
{
	public async Task UpdateAsync(Guid userId, params UserClaim[] claims)
	{
		List<UserClaim> userClaims = await Query(UserClaimSpecification.ByUserId(userId)).ToListAsync();

		foreach (UserClaim item in claims)
		{
			int index = userClaims.FindIndex(x => x.Type == item.Type);
			UserClaim claim = userClaims.Single(x => x.Type == item.Type);

			claim.Type = item.Type;
			claim.Value = item.Value;
			claim.UserId = item.UserId;
			claim.User = item.User;

			userClaims[index] = claim;
		}

		await base.UpdateAsync([.. userClaims]);
	}
}
