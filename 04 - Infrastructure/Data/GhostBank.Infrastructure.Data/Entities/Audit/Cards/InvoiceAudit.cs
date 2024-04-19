namespace GhostBank.Infrastructure.Data.Entities.Audit.Cards;

public class InvoiceAudit : EntityAuditBase
{
	public Guid InvoiceId { get; set; }
}
