using GhostBank.Application.DTOs.Bank;
using GhostBank.Domain.Requests;

namespace GhostBank.Application.Interface.Bank;

public interface IApplicationAccountService : IApplicationServiceBase<AccountDTO, AccountRequest>
{
}
