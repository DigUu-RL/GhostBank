using GhostBank.Infrastructure.Data.Enums.Bank;

namespace GhostBank.Infrastructure.Data.Entities.Bank;

public class Pix : EntityBase
{
	public string Key { get; set; } = null!;
	public PixType Type { get; set; }
	public decimal Limit { get; set; }

	// relationships
	public Guid AccountId { get; set; }
	public Account Account { get; set; } = null!;
}
