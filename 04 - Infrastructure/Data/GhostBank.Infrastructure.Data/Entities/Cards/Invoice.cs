namespace GhostBank.Infrastructure.Data.Entities.Cards;

public class Invoice : EntityBase
{
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsPaid { get; set; }

    // relationships
    public Guid CardId { get; set; }
    public CreditCard Card { get; set; } = null!;
}
