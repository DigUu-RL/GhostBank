using GhostBank.Infrastructure.Data.Entities.Bank;

namespace GhostBank.Infrastructure.Data.Entities.Cards;

public abstract class Card : EntityBase
{
    public string Number { get; set; } = null!;
    public string VerificationCode { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string Password { get; set; } = null!;

    // relationships
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
}
