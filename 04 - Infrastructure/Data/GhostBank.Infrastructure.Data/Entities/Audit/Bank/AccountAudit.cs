namespace GhostBank.Infrastructure.Data.Entities.Audit.Bank;

public class AccountAudit : EntityAuditBase
{
	public Guid AccountId { get; set; }
}
