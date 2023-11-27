namespace GhostBank.Infrastructure.Data.Entities.Identity;

public class UserClaim(string type, string value) : EntityBase
{
	public string Type { get; set; } = type;
	public string Value { get; set; } = value;

	// relationships
	public Guid UserId { get; set; }
	public User User { get; set; } = null!;
}
