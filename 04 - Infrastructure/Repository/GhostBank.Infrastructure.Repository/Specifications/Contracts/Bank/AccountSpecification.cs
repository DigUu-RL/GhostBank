using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Entities.Cards;
using GhostBank.Infrastructure.Data.Enums.Bank;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;

namespace GhostBank.Infrastructure.Repository.Specifications.Contracts.Bank;

public static class AccountSpecification
{
	/// <summary>
	/// Find by <see cref="Account"/> id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public static Specification<Account> ById(Guid id)
	{
		return new AdHocSpecification<Account>(x => x.Id == id);
	}

	/// <summary>
	/// Find by <see cref="Account.Agency"/>
	/// </summary>
	/// <param name="agency"></param>
	/// <returns></returns>
	public static Specification<Account> ByAgency(string agency)
	{
		return new AdHocSpecification<Account>(x => x.Agency == agency);
	}

	/// <summary>
	/// Find by <see cref="Account.Number"/>
	/// </summary>
	/// <param name="number"></param>
	/// <returns></returns>
	public static Specification<Account> ByNumber(string number)
	{
		return new AdHocSpecification<Account>(x => x.Number == number);
	}

	/// <summary>
	/// Find by <see cref="Account.Balance"/>
	/// </summary>
	/// <param name="balance"></param>
	/// <returns></returns>
	public static Specification<Account> ByBalance(decimal balance)
	{
		return new AdHocSpecification<Account>(x => x.Balance == balance);
	}

	/// <summary>
	/// Find by <see cref="Account.Type"/>
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static Specification<Account> ByType(AccountType type)
	{
		return new AdHocSpecification<Account>(x => x.Type == type);
	}

	/// <summary>
	/// Find by <see cref="Account.UserId"/>
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	public static Specification<Account> ByUser(Guid userId)
	{
		return new AdHocSpecification<Account>(x => x.UserId == userId);
	}

	/// <summary>
	/// Find <see cref="Account"/> by some <see cref="Pix"/>
	/// </summary>
	/// <param name="pix</param>
	/// <returns></returns>
	public static Specification<Account> ByPix(Pix pix)
	{
		return new AdHocSpecification<Account>(x => x.Pix.Contains(pix));
	}

	/// <summary>
	/// Find <see cref="Account"/> by some <see cref="Pix"/> id
	/// </summary>
	/// <param name="pixId</param>
	/// <returns></returns>
	public static Specification<Account> ByPix(Guid pixId)
	{
		return new AdHocSpecification<Account>(x => x.Pix.Any(y => y.Id == pixId));
	}

	/// <summary>
	/// Find <see cref="Account"/> by some <see cref="Card"/>
	/// </summary>
	/// <param name="card"></param>
	/// <returns></returns>
	public static Specification<Account> ByCard(Card card)
	{
		return new AdHocSpecification<Account>(x => x.Cards.Contains(card));
	}

	/// <summary>
	/// Find <see cref="Account"/> by some <see cref="Card"/> id
	/// </summary>
	/// <param name="cardId"></param>
	/// <returns></returns>
	public static Specification<Account> ByCard(Guid cardId)
	{
		return new AdHocSpecification<Account>(x => x.Cards.Any(y => y.Id == cardId));
	}
}
