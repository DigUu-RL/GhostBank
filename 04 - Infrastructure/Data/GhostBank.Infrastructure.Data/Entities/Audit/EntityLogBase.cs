using GhostBank.Infrastructure.Data.Enums.Audit;

namespace GhostBank.Infrastructure.Data.Entities.Audit;

public class EntityLogBase
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Column { get; set; } = null!;
    public string OldValue { get; set; } = null!;
    public string NewValue { get; set; } = null!;
    public string Actor { get; set; } = null!;
    public SqlAction Action { get; set; }
}
