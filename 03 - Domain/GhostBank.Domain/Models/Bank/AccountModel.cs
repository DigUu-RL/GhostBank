using GhostBank.Domain.Attributes;
using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Enums.Bank;

namespace GhostBank.Domain.Models.Bank;

[Model]
public class AccountModel
{
	public Guid Id { get; set; }
	public string Agency { get; set; } = null!;
	public string Number { get; set; } = null!;
	public decimal Balance { get; set; }
	public AccountType Type { get; set; }

	public AccountModel()
	{

	}

	public AccountModel(Account entity)
	{
		Id = entity.Id;
		Agency = entity.Agency;
		Number = entity.Number;
		Balance = entity.Balance;
		Type = entity.Type;
	}
}
