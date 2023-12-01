namespace GhostBank.Infrastructure.Data.Entities.Cards;

public sealed class CreditCard : Card
{
    public decimal Limit { get; set; }

    // relationships
    public ICollection<Invoice> Invoices { get; set; } = [];
}
