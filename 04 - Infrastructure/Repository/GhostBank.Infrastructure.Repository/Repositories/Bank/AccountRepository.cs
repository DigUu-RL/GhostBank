using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Repository.Interfaces.Bank;

namespace GhostBank.Infrastructure.Repository.Repositories.Bank;

public class AccountRepository(GhostBankContext context) : BaseRepository<Account>(context), IAccountRepository
{

}
