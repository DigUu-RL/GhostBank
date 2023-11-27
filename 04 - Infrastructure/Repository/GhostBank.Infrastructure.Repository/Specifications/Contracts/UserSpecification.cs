using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;

namespace GhostBank.Infrastructure.Repository.Specifications.Contracts;

public static class UserSpecification
{
	public static Specification<User> ById(Guid id)
	{
		return new ExpressionSpecification<User>(x => x.Id == id);
	}

	public static Specification<User> ByFirstName(string firstName)
	{
		return new ExpressionSpecification<User>(x => x.FirstName.ToLower() == firstName.ToLower());
	}

	public static Specification<User> ByLastName(string lastName)
	{
		return new ExpressionSpecification<User>(x => x.LastName.ToLower() == lastName.ToLower());
	}

	public static Specification<User> ByUserName(string userName)
	{
		return new ExpressionSpecification<User>(x => x.UserName.ToLower() == userName.ToLower());
	}

	public static Specification<User> ByEmail(string email)
	{
		return new ExpressionSpecification<User>(x => x.Email.ToLower() == email.ToLower());
	}
}
