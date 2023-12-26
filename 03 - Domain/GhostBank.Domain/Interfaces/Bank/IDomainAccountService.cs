using GhostBank.Domain.Models.Bank;
using GhostBank.Domain.Requests;

namespace GhostBank.Domain.Interfaces.Bank;

public interface IDomainAccountService : IDomainServiceBase<AccountModel, AccountRequest>
{
}
