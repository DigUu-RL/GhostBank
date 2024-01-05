using GhostBank.Domain.Attributes;
using GhostBank.Infrastructure.Data.Enums.Bank;

namespace GhostBank.Domain.Requests.Bank;

[Request]
public class AccountRequest
{
    public Guid Id { get; set; }
    public string? Agency { get; set; }
    public string? Number { get; set; }
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
}
