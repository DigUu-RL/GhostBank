using GhostBank.Infrastructure.Data.Enums.Identity;

namespace GhostBank.Infrastructure.Data.Entities.Identity;

public class Address : EntityBase
{
    public string Street { get; set; } = null!;
    public string? Number { get; set; }
    public string District { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public State State { get; set; }

    // relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
