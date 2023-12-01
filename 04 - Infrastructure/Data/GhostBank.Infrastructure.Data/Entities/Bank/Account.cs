using GhostBank.Infrastructure.Data.Entities.Cards;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Data.Enums.Account;

namespace GhostBank.Infrastructure.Data.Entities.Bank;

public class Account : EntityBase
{
    public int Agency { get; set; }
    public int Number { get; set; }
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }

    // relationships 
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Pix> Pix { get; set; } = [];
    public ICollection<Card> Card { get; set; } = [];
}
