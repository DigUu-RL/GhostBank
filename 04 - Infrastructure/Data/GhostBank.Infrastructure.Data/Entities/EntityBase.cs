namespace GhostBank.Infrastructure.Data.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Excluded { get; set; }
}
