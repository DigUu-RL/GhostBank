using GhostBank.Domain.Models;
using GhostBank.Domain.Requests;

namespace GhostBank.Domain.Interfaces;

public interface IDomainAccountService : IDomainServiceBase<AccountModel, AccountRequest>
{
}
