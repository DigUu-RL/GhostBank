using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Bank;
using GhostBank.Infrastructure.Repository.Interfaces;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class AccountRepository(GhostBankContext context) : BaseRepository<Account>(context), IAccountRepository
{

}
