using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;

namespace GhostBank.Infrastructure.Repository.Specifications.Contracts;

public static class UserSpecification
{
	public static Specification<User> ById(Guid id)
	{
		return new AdHocSpecification<User>(x => x.Id == id);
	}

	public static Specification<User> ByName(string name)
	{
		return new AdHocSpecification<User>(x => x.Name.ToLower() == name.ToLower());
	}

	public static Specification<User> ByCPF(string cpf)
	{
		return new AdHocSpecification<User>(x => x.CPF == cpf);
	}

	public static Specification<User> ByCNPJ(string cnpj)
	{
		return new AdHocSpecification<User>(x => x.CNPJ == cnpj);
	}

	public static Specification<User> ByUserName(string userName)
	{
		return new AdHocSpecification<User>(x => x.UserName.ToLower() == userName.ToLower());
	}

	public static Specification<User> ByEmail(string email)
	{
		return new AdHocSpecification<User>(x => x.Email.ToLower() == email.ToLower());
	}

	public static Specification<User> ByCellphone(string cellphone)
	{
		return new AdHocSpecification<User>(x => x.Cellphone == cellphone);
	}
}
