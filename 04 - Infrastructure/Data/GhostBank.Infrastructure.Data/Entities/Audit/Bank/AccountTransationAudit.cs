namespace GhostBank.Infrastructure.Data.Entities.Audit.Bank;

public class AccountTransationAudit : EntityAuditBase
{
	public Guid AccountTransactionId { get; set; }
}
