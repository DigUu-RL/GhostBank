using GhostBank.Infrastructure.Data.Enums.Bank;

namespace GhostBank.Infrastructure.Data.Entities.Bank;

public class Transaction : EntityBase
{
	public decimal Amount { get; set; }
	public string? Description { get; set; }
	public TransactionType Type { get; set; }

	// relationships
	public ICollection<AccountTransaction> Accounts { get; set; } = [];
}
