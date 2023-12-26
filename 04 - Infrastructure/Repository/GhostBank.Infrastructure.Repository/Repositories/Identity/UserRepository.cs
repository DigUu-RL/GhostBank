using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;

namespace GhostBank.Infrastructure.Repository.Repositories.Identity;

public class UserRepository(GhostBankContext context) : BaseRepository<User>(context), IUserRepository
{

}
