using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;

namespace GhostBank.Infrastructure.Repository.Specifications.Contracts;

public static class UserClaimSpecification
{
	public static Specification<UserClaim> ById(Guid id)
	{
		return new ExpressionSpecification<UserClaim>(x => x.Id == id);
	}

	public static Specification<UserClaim> ByUserId(Guid userId)
	{
		return new ExpressionSpecification<UserClaim>(x => x.UserId == userId);
	}
}
