using GhostBank.Domain.Models.Bank;
using GhostBank.Domain.Requests.Bank;

namespace GhostBank.Domain.Interfaces.Bank;

public interface IDomainAccountService : IDomainServiceBase<AccountModel, AccountRequest>
{
}
