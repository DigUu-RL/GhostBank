namespace GhostBank.Infrastructure.Data.Entities.Audit.Identity;

public class UserAudit : EntityAuditBase
{
	public Guid UserId { get; set; }
}
