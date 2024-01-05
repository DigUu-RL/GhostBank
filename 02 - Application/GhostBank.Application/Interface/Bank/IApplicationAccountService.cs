using GhostBank.Application.DTOs.Bank;
using GhostBank.Domain.Requests.Bank;

namespace GhostBank.Application.Interface.Bank;

public interface IApplicationAccountService : IApplicationServiceBase<AccountDTO, AccountRequest>
{
}
