using GhostBank.Infrastructure.Data.Entities.Cards;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Data.Enums.Account;

namespace GhostBank.Infrastructure.Data.Entities.Bank;

public class Account : EntityBase
{
    public string Agency { get; set; } = null!;
    public string Number { get; set; } = null!;
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }

    // relationships 
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Pix> Pix { get; set; } = [];
    public ICollection<Card> Cards { get; set; } = [];
}
