namespace GhostBank.Infrastructure.Data.Entities.Bank;

public class AccountTransaction : EntityBase
{
	public Guid TransactionId { get; set; }
	public Transaction Transaction { get; set; } = null!;
	public Guid AccountId { get; set; }
	public Account Account { get; set; } = null!;
}
