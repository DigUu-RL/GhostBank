using GhostBank.Domain.Attributes;
using GhostBank.Domain.Models.Bank;
using GhostBank.Infrastructure.Data.Enums.Bank;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace GhostBank.Application.DTOs.Bank;

[DTO]
public class AccountDTO
{
	public string? Agency { get; set; }
	public string? Number { get; set; }
	public decimal Balance { get; set; }
	public AccountType Type { get; set; }

    public AccountDTO()
    {
        
    }

    public AccountDTO(AccountModel model)
    {
        Agency = model.Agency;
        Number = model.Number;
        Balance = model.Balance;
        Type = model.Type;
    }
}
