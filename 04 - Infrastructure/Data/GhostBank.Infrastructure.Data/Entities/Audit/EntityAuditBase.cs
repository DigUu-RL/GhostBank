using GhostBank.Infrastructure.Data.Enums.Audit;

namespace GhostBank.Infrastructure.Data.Entities.Audit;

public class EntityAuditBase
{
	public Guid Id { get; set; }
	public DateTime Date { get; set; }
	public string? Column { get; set; }
	public string? OldValue { get; set; }
	public string? NewValue { get; set; }
	public string Actor { get; set; } = null!;
	public SqlAction Action { get; set; }
}
