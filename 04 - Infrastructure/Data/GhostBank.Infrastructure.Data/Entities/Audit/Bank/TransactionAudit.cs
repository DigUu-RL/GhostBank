namespace GhostBank.Infrastructure.Data.Entities.Audit.Bank;

public class TransactionAudit : EntityAuditBase
{
	public Guid TransactionId { get; set; }
}
