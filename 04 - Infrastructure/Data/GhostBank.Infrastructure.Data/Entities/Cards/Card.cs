using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Data.Enums.Cards;

namespace GhostBank.Infrastructure.Data.Entities.Cards;

public class Card : EntityBase
{
	public string Number { get; set; } = null!;
	public string VerificationCode { get; set; } = null!;
	public DateTime ExpirationDate { get; set; }
	public string Password { get; set; } = null!;
	public CardType Type { get; set; }

	// relationships
	public Guid AccountId { get; set; }
	public Account Account { get; set; } = null!;
}
