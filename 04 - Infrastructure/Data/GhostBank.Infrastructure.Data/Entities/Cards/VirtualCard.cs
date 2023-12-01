namespace GhostBank.Infrastructure.Data.Entities.Cards;

public sealed class VirtualCard : Card
{
    public bool IsCredit { get; set; }
    public decimal? Limit { get; set; }
}
